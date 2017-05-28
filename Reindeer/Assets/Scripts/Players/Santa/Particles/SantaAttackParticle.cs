using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaAttackParticle : MonoBehaviour {

    //particle variables
    public GameObject impactParticle; //ref to impact particle prefab

    private ParticleCollisionEvent collisionEvent; //ref to particle collision event, needed for collision detection and further logic

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnParticleCollision(GameObject other)
    {
        //print(other.gameObject.name);
        //check for reindeer
        if (other.CompareTag("Reindeer"))
        {
            //print(other.gameObject.name);
            //decrease the health of reindeer
            other.gameObject.GetComponent<Reindeer>().DecreaseHealth(10);
            //get position of collision
            Vector3 collisionPos = collisionEvent.intersection;
            //reset the rotation
            Quaternion CorrectedRotation = Quaternion.identity;
            //adjust rotation
            CorrectedRotation.eulerAngles = transform.rotation.eulerAngles + new Vector3(0.0f, 90.0f, 0.0f);
            //create clone of particle effect prefab
            GameObject impactClone = impactParticle;
            //instantiate prefab into scene
            Instantiate(impactClone, collisionPos, CorrectedRotation);
            //destroy the original prefab projectile
            Destroy(gameObject);
        }

    }
}
