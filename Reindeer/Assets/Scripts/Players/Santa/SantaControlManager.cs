using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using XInputDotNetPure;
using UnityEngine.SceneManagement;

public class SantaControlManager : MonoBehaviour {

    public GameObject santa; //reference to santa object
    public Transform[] spawnPoints = new Transform[8];
    public Transform startSpawn; //reference to starting spawn point for santa
    public GameObject spawnCursor; //reference to visual cue of respawn choice
    public int lives = 3;

    [HideInInspector]
    public int chosenSpawnpointIndex; //index of chosen spawn point
    [HideInInspector]
    public bool needRespawn = false; //checks if player has respawned

    public bool canInput = true; //check if input is currently being accepted
    
    Transform chosenSpawnpoint; //position of chosen transform position

    //GamePadState state;

    // Use this for initialization
    void Start () {
        SpawnSanta();
	}
	
	// Update is called once per frame
	void Update () {
        //if allowing input
        if (canInput)
        {
            //if out of lives go back to title screen
            if (lives <= 0)
            {
                SceneManager.LoadScene(0);
            }
            //if needs to respawn
            if (needRespawn)
            {
                //get a random spawn point
                GetRandomSpawnPoint();
                //state = GamePad.GetState(PlayerIndex.One);
                //respawn santa
                RespawnSanta();
                //for choosing spawn point
                //if (Input.GetKeyDown(KeyCode.JoystickButton0) || state.Buttons.A == ButtonState.Pressed)
                //{
                    
                //}
            }
        }
        //check if the joystick is free
        else
        {
            CheckJoystickFree();
        }
        
	}

    //respawns santa at the chosen location
    void RespawnSanta()
    {
        //create a clone of the prefab
        GameObject santaClone = santa;
        //instantiate prefab in scene
        Instantiate(santaClone, spawnPoints[chosenSpawnpointIndex].position, spawnPoints[chosenSpawnpointIndex].rotation);
        //set respawning to false
        needRespawn = false;
        //spawnCursor.SetActive(false);
        //remove a life
        lives -= 1;
    }

    //spawns santa in
    void SpawnSanta()
    {
        //create a clone of the prefab
        GameObject santaClone = santa;
        //instantiate prefab in scene
        Instantiate(santaClone, startSpawn.position, startSpawn.rotation);
        //set respawning to false
        needRespawn = false;
        
    }

    //controls to pick respawn location
    void PickRespawnLocation()
    {
        //check if input is allowed
        //canInput = true;
        if (canInput)
        {
            //state = GamePad.GetState(PlayerIndex.One);
            if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 /*|| state.Buttons.A == ButtonState.Pressed*/)
            {
                chosenSpawnpointIndex++;
                if (chosenSpawnpointIndex == 8)
                {
                    chosenSpawnpointIndex = 0;
                }
                canInput = false;
            }
            else if(Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0 /*|| state.Buttons.A == ButtonState.Pressed*/)
            {
                chosenSpawnpointIndex--;
                if (chosenSpawnpointIndex == -1)
                {
                    chosenSpawnpointIndex = 7;
                }
                canInput = false;
            }
        }
    }

    //called when santa first dies
    public void GetRandomSpawnPoint()
    {
        int random = Random.Range(0, 7);
        //chosenSpawnpoint = spawnPoints[random];
        chosenSpawnpointIndex = random;
    }

    //checks if the joystick input is free
    void CheckJoystickFree()
    {
        //if no input, then allow input to change state
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            canInput = true;
        }
    }
}