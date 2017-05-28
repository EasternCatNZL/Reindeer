using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SledAttackTest : MonoBehaviour {

    //spin attack vars
    //public CapsuleCollider myCollider; //ref to objects collider

    private bool isAttacking = false; //checks if attacking

    //present attack vars
    public GameObject presentPrefab; //present prefab

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        //if attacking on
        if (isAttacking)
        {
            //kill things

        }
    }

    //picks a random direction and throws a present
    void ThrowPresent()
    {
        //get input to throw
                
        //get a random y rotation
        float randomYRotation = Random.Range(0, 360);
        //create new quaternion with random y rotation
        Quaternion alteredRotation = new Quaternion();
        alteredRotation.eulerAngles = new Vector3(270.0f, randomYRotation, 0);
        //create clone of object
        GameObject presentClone = presentPrefab;
        //spawn object
        Instantiate(presentClone, transform.position, alteredRotation);
    }

    //spin attack logic
    void SpinAttack()
    {
        //get input

        //set attacking to true
        isAttacking = true;
    }
}
