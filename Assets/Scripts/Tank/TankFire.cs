using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TankFire : MonoBehaviour {

    public int playerNumber = 1;
    public Slider aimSlider;
    public float chargeRate = 1.5f;

    public float turrentRotateSpeed = 2.2f;
    public Transform shootableTargets;
    public GameObject shootableObject;

    public float minDistance = 0f;
    public float maxDistance = 30f;
    public float maxChargeTime = 0.75f;
    public GameObject Shell;
    public bool wait = true;

    private string FireButton;
    private float currentDistance;
    private float chargeSpeed;
    private bool fired;

    private IEnumerator turretMoveShotPos;
    private IEnumerator turretMoveOrigPos;
    GameObject tankTurrent;
    private Vector3 targetDir;
    private Vector3 targetDirection;
    private float step;
    float dotProduct;
    int eventDriver = 0;
    private bool chargeFireForward = true;

    GameObject FirePoint;
    
    
    ProjectileShell projectileShell;
    // private IEnumerator arrow;

	// Use this for initialization
	void Start () {
        tankTurrent = this.transform.Find("Tank/TankRenderers/TankTurret").gameObject;
        // turretMoveShotPos = RotateTurret(shootableTargets.position, turrentRotateSpeed);
        // turretMoveOrigPos = RotateTurret(transform.forward, turrentRotateSpeed);
        turretMoveShotPos = RotateTurret(shootableTargets, turrentRotateSpeed);
        // turretMoveOrigPos = RotateTurret(transform.forward, turrentRotateSpeed);
        FirePoint = this.transform.Find("Tank/TankRenderers/TankTurret/FirePoint").gameObject;
        projectileShell = Instantiate(Shell, gameObject.transform).GetComponent<ProjectileShell>();
        chargeSpeed = (maxDistance - minDistance) / maxChargeTime;
        eventDriver = 0;


    }

    private void OnEnable()
    {
        currentDistance = minDistance;
        aimSlider.value = minDistance;

    }

    public bool FireTank(float time)
    {

        if (distanceOfShootable(shootableTargets) > maxDistance)
        {
            // The shootable target is too far
            return true;
        }
        else
        {
            if (eventDriver == 0)
            {
                // Moving the turret however it moves the tank now
                turretMoveShotPos = RotateTurret(shootableTargets, turrentRotateSpeed);
                if (MoveTurret())
                {
                    eventDriver++;
                }
            }
            else if (eventDriver == 1)
            {
                if (DelayedFire(time))
                {
                    eventDriver++;
                }
                wait = false;
            }
            else if (eventDriver == 2)
            {
                wait = true;
                fired = false;
                chargeFireForward = true;
                aimSlider.value = minDistance;
                eventDriver = 0;
                return true;
            }
        }
        return false;
            
    }
   

    public bool MoveTurret()
    {
        // turretMove = RotateTurret(shootableGameObject.transform.position, turrentRotateSpeed);
        
        StartCoroutine(turretMoveShotPos);
        targetDirection = shootableTargets.position - transform.position;
        // dotProduct = Vector3.Dot(transform.forward, targetDirection.normalized);
        dotProduct = Vector3.Dot(tankTurrent.transform.forward, targetDirection.normalized);
        if (dotProduct >= 0.95f)
        {
            return true;
        }
        return false;
    }

    public bool DelayedFire(float time)
    {
        
        // Debug.Log("Waiting for fire ");
        if (fired)
        {
            Debug.Log("Firing");
            Invoke("Fire", time);
            return true;
        }
        
        return false;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (wait)
        {

        }
        else
        {
            if (Input.GetButtonDown("Fire1") && !fired)
            {
                fired = false;
            }
            else if (Input.GetButton("Fire1") && !fired)
            {
                if (chargeFireForward)
                {
                    currentDistance += chargeSpeed * Time.deltaTime;
                }
                else
                {
                    currentDistance -= chargeSpeed * Time.deltaTime;
                }
                
                aimSlider.value = currentDistance;
                if (currentDistance >= maxDistance)
                {
                    chargeFireForward = false;
                }
                else if (currentDistance <= minDistance)
                {
                    chargeFireForward = true;
                }
            }
            else if (Input.GetButtonUp("Fire1") && !fired)
            {
                fired = true;
            }
        }
        

    }

    public bool Fire()
    {
        //Debug.Log("Fires from tankFire");
        //aimSlider.value = minDistance;
        //projectileShell.Fire(FirePoint.transform, shootableTargets);
        float dist = Vector3.Distance(FirePoint.transform.position, shootableTargets.position);
        float percentDist = currentDistance / dist;
        Vector3 LerpPosition = Vector3.Lerp(transform.position, shootableTargets.position, percentDist);
        projectileShell.FireArrow(FirePoint.transform.position, LerpPosition);
        return true;
    }

    public float distanceOfShootable(Transform trans)
    {
        return Vector3.Distance(trans.position, transform.position);
    }

    
    IEnumerator RotateTurret(Transform end, float rotateSpeed)
    {

        targetDir = (end.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(targetDir);
        float timer = Time.time;
        while (Time.time < timer + rotateSpeed)
        {
            
            step = Time.deltaTime * rotateSpeed;
            // transform.rotation = Quaternion.Lerp(transform.rotation, rotation, step);
            tankTurrent.transform.rotation = Quaternion.Lerp(tankTurrent.transform.rotation, rotation, step);
            // Debug.Log("rotation dir" + rotation.eulerAngles.ToString());
            yield return null;
        }
        

    }

}
