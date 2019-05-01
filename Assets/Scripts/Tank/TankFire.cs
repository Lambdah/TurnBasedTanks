using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankFire : MonoBehaviour {

    public int playerNumber = 1;
    public Slider aimSlider;
    public float chargeRate;

    public float turrentRotateSpeed;
    public float shootableRange;
    public Transform shootableTargets;

    public float minDistance = 0f;
    public float maxDistance = 30f;
    public float maxChargeTime = 0.75f;
    public GameObject Shell;

    private string FireButton;
    private float currentDistance;
    private float chargeSpeed;
    private bool fired;

    private IEnumerator turrentMove;
    GameObject tankTurrent;
    private Vector3 startDir;
    private Vector3 targetDir;
    private Vector3 newDir;
    private float step;
    float dotProduct;

    GameObject FirePoint;
    int playerTurn;
    
    ProjectileShell projectileShell;
    private IEnumerator arrow;

	// Use this for initialization
	void Start () {
        tankTurrent = this.transform.Find("TankRenderers/TankTurret").gameObject;
        turrentMove = RotateTurret(shootableTargets.position, turrentRotateSpeed);
        FirePoint = this.transform.Find("TankRenderers/TankTurret/FirePoint").gameObject;
        projectileShell = Shell.GetComponent<ProjectileShell>();

        chargeSpeed = (maxDistance - minDistance) / maxChargeTime;
        //arrow = GrowArrow();
    }

    private void OnEnable()
    {
        currentDistance = minDistance;
        aimSlider.value = minDistance;

    }

   

    public bool MoveTurret()
    {
        StartCoroutine(turrentMove);

        dotProduct = Vector3.Dot(tankTurrent.transform.forward, (shootableTargets.position - tankTurrent.transform.forward).normalized);
        if (dotProduct >= 0.95f)
        {
            return false;
        }
        return true;
    }

    public bool DelayedFire(float time, int playerNum)
    {
        playerTurn = playerNum;
        Debug.Log("Waiting for fire ");
        if (fired && playerTurn == playerNumber)
        {
            Debug.Log("Firing");
            Invoke("Fire", time);
            return true;
        }
        
        return false;
    }

    private void Update()
    {
        
        if (Input.GetButtonDown("Fire1") && playerTurn == playerNumber && !fired)
        {
            fired = false;
        }
        else if(Input.GetButton("Fire1") && !fired && playerTurn == playerNumber)
        {
            Debug.Log("Button being held down");
            currentDistance += chargeSpeed * Time.deltaTime;
            aimSlider.value = currentDistance;
        }
        else if (Input.GetButtonUp("Fire1") && !fired && playerTurn == playerNumber)
        {
            fired = true;
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

    


    IEnumerator RotateTurret(Vector3 end, float rotateSpeed)
    {
        targetDir = tankTurrent.transform.position - end;
        targetDir = new Vector3(targetDir.x, 0f, targetDir.z);
        float turnAngle = Vector3.SignedAngle(targetDir, -transform.forward, transform.up);
        Debug.Log("Turn angle "+ turnAngle);
        aimSlider.transform.Rotate(Vector3.forward, turnAngle);
        float timer = Time.time;
        while (Time.time < timer + rotateSpeed)
        {
            step = Time.deltaTime * rotateSpeed;
            newDir = Vector3.RotateTowards(tankTurrent.transform.forward, -targetDir, step, 0f);
            tankTurrent.transform.rotation = Quaternion.LookRotation(newDir);
            yield return null;
        }

    }

}
