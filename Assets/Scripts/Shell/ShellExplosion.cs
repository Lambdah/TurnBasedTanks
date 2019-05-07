using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    // public LayerMask m_TankMask;
    // public ParticleSystem m_ExplosionParticles;       
    // public AudioSource m_ExplosionAudio;              
    // public float m_MaxDamage = 100f;                  
    // public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;              


    private void Start()
    {
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.
        if (other)
        {
            gameObject.SetActive(false);
        }
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        return 0f;
    }
}