using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaKill : MonoBehaviour {

    public SantaControlManager santaManager; //reference to santa manager script

	// Use this for initialization
	void Start () {
        santaManager = GameObject.Find("GameManager").GetComponent<SantaControlManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //call to kill santa
    public void KillSanta()
    {
        //destroys santa game object
        Destroy(gameObject);
        //move respawn to random spawn location
        santaManager.GetRandomSpawnPoint();
        //tells manager santa needs to respawn
        santaManager.needRespawn = true;
    }
}
