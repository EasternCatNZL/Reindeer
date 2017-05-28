using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaGameplayTestScript : MonoBehaviour {

    public SantaKill killScript; //reference to santa kill script

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (killScript == null)
            {
                killScript = GameObject.FindGameObjectWithTag("Santa").GetComponent<SantaKill>();
            }
            killScript.KillSanta();
        }
	}
}
