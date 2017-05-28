using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    //movement vars
    public float speed; //speed at which projectile travels
    //public float bulletRange = 5.0f; //the max range that a projectile can fly

    //private float bulletDistance = 0.0f; //current distance of projectile

    //shot effects vars
    public AudioSource shot; //reference to shotting sound effect

    //game object refs
    public GameObject impactParticle; //reference to the impact particle prefab

    //particle ref
    private float particleDuration; //duration of this particle systems lifespan
    private ParticleSystem partsSystem; //reference to objects particle system
    

    // Use this for initialization
    void Start () {
        partsSystem = GetComponent<ParticleSystem>();
        particleDuration = partsSystem.main.duration + partsSystem.main.startLifetimeMultiplier;
        //startTime = Time.time;
        shot.Play();
        Destroy(gameObject, particleDuration);
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

}
