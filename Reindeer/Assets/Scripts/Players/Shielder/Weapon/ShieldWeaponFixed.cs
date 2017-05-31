using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWeaponFixed : MonoBehaviour
{

    //attack vars
    [Header("Attack Values")]
    public float damage; //amount of damage weapon deals
    public bool isActive; //checks to see if weapon is active
    //[HideInInspector]
    public bool isFirstSecondHit; //checks if currently first or second attack
   // [HideInInspector]
    public bool isThirdHit; //checks if currently third hit
    public GameObject ReindeerRef = null;

    //particles
    [Header("Particles")]
    public GameObject firstSecondImpactParticle; //ref to impact particle for first and second hit
    public GameObject thirdImpactParticle; //ref to impact particle for third hit

    //audio
    [Header("Audio")]
    public AudioSource impactSound;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && ReindeerRef)
        {

            if (isFirstSecondHit)
            {
                GameObject firstSecondClone = firstSecondImpactParticle;
                Instantiate(firstSecondClone, ReindeerRef.transform.position, ReindeerRef.transform.rotation);
                ReindeerRef.GetComponent<HealthManagement>().DecreaseHealth(damage);
                isFirstSecondHit = false;
            }
            else if (isThirdHit)
            {
                GameObject thirdClone = thirdImpactParticle;
                Instantiate(thirdClone, ReindeerRef.transform.position, ReindeerRef.transform.rotation);
                ReindeerRef.GetComponent<HealthManagement>().DecreaseHealth(damage);
                isThirdHit = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reindeer")
        {
            ReindeerRef = other.gameObject;
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

            //vary pitch
            impactSound.pitch = Random.Range(0.6f, 1.0f);
            //play sound
            impactSound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ReindeerRef && other.gameObject.tag == "Reideer")
        {
            ReindeerRef = null;
        }
    }
}
