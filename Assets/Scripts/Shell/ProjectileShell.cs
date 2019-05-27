using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShell : MonoBehaviour {
    
    public Transform targetPosition;
    public bool collided;
    public float degrees;
    public GameObject shellExplosion_prefab;
    public AudioSource explosionAudio;
    public float shellDamage = 20f;
    

    
    Vector3 target;
    private Rigidbody rb;
    Vector3 velocity_obj;
    private ParticleSystem shellExplosion;
    private AudioSource shellExplosionAudio;
    private CameraFollow cam;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    public void SetStart()
    {
        // A lot of null reference errors occur with shellExplosion at the start of the game
        // when this code was in Start() function. So moved it to this function and have TankFire() call 
        // this function when gameobject is instantiated
        shellExplosion = Instantiate(shellExplosion_prefab).GetComponent<ParticleSystem>();
        shellExplosionAudio = shellExplosion_prefab.GetComponent<AudioSource>();
        shellExplosion.gameObject.SetActive(false);
        collided = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        rb = GetComponent<Rigidbody>();
    }
    
    public void FireArrow(Vector3 startPos, Vector3 Arrow)
    {
        shellExplosion.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
        //velocity_obj = BallisticVelocityVector(transform.position, target, degrees);
        transform.position = startPos;
        velocity_obj = BallisticVelocityVector(transform.position, Arrow, degrees);
        rb.velocity = velocity_obj;
        cam.ChaseAction(this.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Debug.Log("Collision " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Node" || collision.gameObject.tag == "Player")
        {
            
            shellExplosion.gameObject.SetActive(true);
            shellExplosion.gameObject.transform.position = collision.gameObject.transform.position;
            shellExplosion.Play();
            // Get a warning about the gameobject is deactivated and playing the sound.
            // Think about creating a SoundManager and just play it from there to get
            // rid of the warning. However, the sound still plays
            shellExplosionAudio.Play();
            cam.StopChase(this.gameObject.transform.position);
            collided = true;
        }
        
        this.gameObject.SetActive(false);

    }

    Vector3 BallisticVelocityVector(Vector3 start, Vector3 target, float angle)
    {
        Vector3 direction = target - start;
        float height = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float theta = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(theta);
        distance += height / Mathf.Tan(theta);
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * theta));
        return velocity * direction.normalized;
    }

    

}
