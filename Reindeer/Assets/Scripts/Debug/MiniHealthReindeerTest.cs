using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniHealthReindeerTest : MonoBehaviour {

    public bool SpawnActive = false;
    public int MaximumPop = 10; //Maximum amount of Mini Reindeer allowed
    //spawning vars
    public int spawnChance = 65; //percentage of spawn chance
    public float spawnDelay = 2.0f; //rate at which spawn attempts happen
    public Transform[] spawnPoints = new Transform[8]; //points at which minions can spawn

    private float lastSpawnTime = 0.0f; //time of last spawn attempt

    private static int MiniReindeerCount = 0;

    //object ref vars
    public GameObject miniReindeer; //ref to mini reindeer prefab

	// Use this for initialization
	void Start () {
        lastSpawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (SpawnActive)
        {
            SpawnMini();
        }
	}

    //spawns mini reindeer using random chance at random spawn
    void SpawnMini()
    {
        //if delay has passed since last spawn attempt
        if (Time.time >= lastSpawnTime + spawnDelay && MiniReindeerCount < MaximumPop)
        {
            //get a random number
            int randomNum = Random.Range(0, 100);
            //if random number within given spawn chance
            if (randomNum <= spawnChance)
            {
                //get a spawn point
                int randomSpawn = Random.Range(0, spawnPoints.Length);
                //create a clone of mini reindeer object
                GameObject miniClone = miniReindeer;
                //spawn at random spawn point
                Instantiate(miniClone, spawnPoints[randomSpawn].transform.position, spawnPoints[randomSpawn].transform.rotation);

                MiniReindeerCount++;
            }
            //set last attempt time to current time
            lastSpawnTime = Time.time;
        }
    }

    public static void MiniReindeerDeath()
    {
        MiniReindeerCount--;
    }
}
