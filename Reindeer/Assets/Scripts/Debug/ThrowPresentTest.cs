using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPresentTest : MonoBehaviour {

    //present var
    public GameObject presentPrefab; //present prefab

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ThrowPresent();
	}

    void ThrowPresent()
    {
        //get input to throw
        if (Input.GetKeyDown(KeyCode.E))
        {
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
    }
}
