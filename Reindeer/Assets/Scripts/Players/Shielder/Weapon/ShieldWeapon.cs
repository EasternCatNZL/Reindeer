using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWeapon : MonoBehaviour {

    //attack vars
    [Header("Attack Values")]
    public float damage; //amount of damage weapon deals
    public bool isActive; //checks to see if weapon is active
    [HideInInspector]
    public bool isFirstSecondHit; //checks if currently first or second attack
    [HideInInspector]
    public bool isThirdHit; //checks if currently third hit

    //particles
    [Header("Particles")]
    public GameObject firstSecondImpactParticle; //ref to impact particle for first and second hit
    public GameObject thirdImpactParticle; //ref to impact particle for third hit

    //audio
    [Header("Audio")]
    public AudioSource impactSound;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
		print ("TRIGGER");
        //check if active
        if (isActive)
        {
            if (other.CompareTag("Reindeer"))
            {
                //do damage
				if (other.gameObject.tag == "Reindeer") 
				{
					other.gameObject.GetComponent<HealthManagement> ().DecreaseHealth (damage);
				}
                //spawn particle effects
                if (isFirstSecondHit)
                {
                    GameObject firstSecondClone = firstSecondImpactParticle;
                    Instantiate(firstSecondClone, other.transform.position, other.transform.rotation);
                }
                else if (isThirdHit)
                {
                    GameObject thirdClone = thirdImpactParticle;
                    Instantiate(thirdClone, other.transform.position, other.transform.rotation);
                }
            }
            else if (other.CompareTag("Mini"))
            {
                //destroy mini reindeer
                Destroy(other.gameObject);

                //spawn particle effects
                if (isFirstSecondHit)
                {
                    GameObject firstSecondClone = firstSecondImpactParticle;
                    Instantiate(firstSecondClone, other.transform.position, other.transform.rotation);
                }
                else if (isThirdHit)
                {
                    GameObject thirdClone = thirdImpactParticle;
                    Instantiate(thirdClone, other.transform.position, other.transform.rotation);
                }
            }
            //vary pitch
            impactSound.pitch = Random.Range(0.6f, 1.0f);
            //play sound
            impactSound.Play();
        }
    }
}
