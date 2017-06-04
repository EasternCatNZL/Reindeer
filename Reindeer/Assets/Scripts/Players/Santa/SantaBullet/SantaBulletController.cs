using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaBulletController : MonoBehaviour {

    //movement physics vars
    [Header("Physics vars")]
    public float moveSpeed = 2.0f; //speed at which bullet flies

    //bullet control vars
    [Header("Control vars")]
    public float maxDistance = 10.0f; //max distance bullet can fly
    //public float maxDuration = 5.0f; //max time bullet can fly

    private float currentDistance = 0.0f; //current traveled distance
    //private float currentDuration = 0.0f; //current lifetime
    private Vector3 startPos; //starting position

    //bullet interaction behaviour
    public float damage = 10.0f; //amount of damage bullet does

    //particle var
    [Header("Particle")]
    public GameObject impactParticle; //ref to impact particle

	// Use this for initialization
	void Start () {
        //get start pos
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        TrackLifetime();
	}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //if collide with reindeer, decrease reindeer health
    //    if (collision.gameObject.CompareTag("Reindeer"))
    //    {
    //        collision.gameObject.GetComponent<Reindeer>().DecreaseHealth(damage);
    //        //spawn impact particle
    //        GameObject impactClone = impactParticle;
    //        //instantiate prefab into scene
    //        Instantiate(impactClone, transform.position, transform.rotation);
    //        //destroy the bullet
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        //if collide with reindeer, decrease reindeer health
        if (other.CompareTag("Reindeer"))
        {
            other.GetComponent<Reindeer>().DecreaseHealth(damage);
            //spawn impact particle
            GameObject impactClone = impactParticle;
            //instantiate prefab into scene
            Instantiate(impactClone, transform.position, transform.rotation);
            //destroy the bullet
            Destroy(gameObject);
        }
        //if collide with arena
        if (other.CompareTag("Arena"))
        {
            //spawn impact particle
            GameObject impactClone = impactParticle;
            //instantiate prefab into scene
            Instantiate(impactClone, transform.position, transform.rotation);
            //destroy the bullet
            Destroy(gameObject);
        }
    }

    //move the bullet forward
    private void Move()
    {
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    }

    //track lifetime values
    private void TrackLifetime()
    {
        //if distance traveled is greater than max distance
        if (Vector3.Distance(transform.position, startPos) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
