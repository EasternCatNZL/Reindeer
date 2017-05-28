using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniReindeerMove : MonoBehaviour {

    //movement vars
    public float moveSpeed = 3.0f; //speed at which object moves

    //main reindeer ref
    private GameObject mainReindeer; //ref to main reindeer's transform

	// Use this for initialization
	void Start () {
        mainReindeer = GameObject.FindGameObjectWithTag("Reindeer");
	}
	
	// Update is called once per frame
	void Update () {
        MoveTowardsReindeer();
	}

    //move towards main reindeer
    void MoveTowardsReindeer()
    {
		GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, mainReindeer.transform.position, moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if collising with reindeer
        if (collision.gameObject.CompareTag("Reindeer"))
        {
            //do heal reindeer
        }
    }

    void OnDestroy()
    {
        MiniHealthReindeerTest.MiniReindeerDeath();
    }
}
