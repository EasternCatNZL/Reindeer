using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour {

    ScreenSplat splat;  

	// Use this for initialization
	void Start ()
    {
        splat = GameObject.Find("Image").GetComponent<ScreenSplat>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Attack();       
		
	}

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("pressed");
            splat.image.enabled = true;
            splat.GetComponent < ScreenSplat>().ResetAlpha();
            print("resetting alpha");
            splat.GetComponent<ScreenSplat>().FadeToTransparent();
            print("fading");        
        }
    }
}
