using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileParticle : MonoBehaviour {

	[Header("Ability Damage")]
	public float damage = 5.0f;
    //audio
    [Header("Audio")]
    public AudioSource shotSound; //ref to audio source of bullet being fired

    //particles
    [Header("Particles")]
    public GameObject impactParticle; //ref to impact particle of this missile

    private float particleDuration; //duration of this particles lifespan
    private ParticleSystem partsSystem; //ref to objects particle system
    private ParticleCollisionEvent collisionEvent; //ref to particle collision event, needed for collision detection and further logic

    // Use this for initialization
    void Start () {
		partsSystem = GetComponent<ParticleSystem>();
        //get duration
        particleDuration = partsSystem.main.duration + partsSystem.main.startLifetimeMultiplier;
        //play audio

        //destroy if flies for set lifetime without impacting
        Destroy(gameObject, particleDuration);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        //get point of collision
        Vector3 collisionPos = collisionEvent.intersection;
        //create impact particle at location
        GameObject impactClone = impactParticle;
        Instantiate(impactClone, collisionPos, transform.rotation);
        //check the object that has been collided into
        if (other.CompareTag("Reindeer"))
        {
			other.GetComponent<HealthManagement> ().DecreaseHealth (damage);
        }
        else if (other.CompareTag("Mini"))
        {

        }
        else if (other.CompareTag("Arena"))
        {

        }
        //once done, destroy self
        Destroy(gameObject);
    }
}
