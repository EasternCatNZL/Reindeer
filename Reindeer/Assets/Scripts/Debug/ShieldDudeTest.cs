using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDudeTest : MonoBehaviour {

    //script ref
    private GenericPlayerControlTest playerController; //ref to player control

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        IsShieldUp();
	}
    
    //set shield
    void IsShieldUp()
    {
        //if special input is held down, set can take damage to false
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerController.canTakeDamage = false;
        }
        else
        {
            playerController.canTakeDamage = true;
        }
    }
}
