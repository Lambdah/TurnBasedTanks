using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShell : MonoBehaviour {
    
    public Transform targetPosition;
    public bool fire;
    public bool collided;
    public float degrees;
    public ParticleSystem shellExplosion;
    public AudioSource explosionAudio;
    

    
    Vector3 target;
    private Rigidbody rb;
    Vector3 velocity_obj;
    
    // Use this for initialization
    void Start () {
        fire = false;
        collided = false;
        this.gameObject.SetActive(false);
        shellExplosion.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    public void Fire(Transform startPos, Transform targetPos)
    {
        this.gameObject.SetActive(true);
        rb = GetComponent<Rigidbody>();
        //velocity_obj = BallisticVelocityVector(transform.position, target, degrees);
        transform.position = startPos.position;
        velocity_obj = BallisticVelocityVector(transform.position, targetPos.position, degrees);
        rb.velocity = velocity_obj;
        fire = true;
    }

    public void FireArrow(Vector3 startPos, Vector3 Arrow)
    {
        shellExplosion.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
        rb = GetComponent<Rigidbody>();
        //velocity_obj = BallisticVelocityVector(transform.position, target, degrees);
        transform.position = startPos;
        velocity_obj = BallisticVelocityVector(transform.position, Arrow, degrees);
        rb.velocity = velocity_obj;
        fire = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Hit the collider: " + collision.gameObject.tag.ToString());
        this.gameObject.SetActive(false);
        shellExplosion.gameObject.SetActive(true);
        shellExplosion.Play();
        // explosionAudio.Play();
        fire = false;
        collided = true;
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
