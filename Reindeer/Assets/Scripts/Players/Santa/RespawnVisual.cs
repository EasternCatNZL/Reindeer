using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnVisual : MonoBehaviour {

    public float rotateSpeed = 5.0f;
    public SantaControlManager santaManager; //reference to santa manager

	// Use this for initialization
	void Start () {
        santaManager = GameObject.Find("GameManager").GetComponent<SantaControlManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (santaManager.needRespawn)
        {
            Turn();
            FollowChoice();
        }
	}

    //shows visual indicator
    public void ShowIndicator()
    {
        this.GetComponent<Material>().color = new Color(0.7f, 0.2f, 0.2f, 1.0f);
    }

    //hides visual indicator
    public void HideIndicator()
    {
        this.GetComponent<Material>().color = new Color(0.7f, 0.2f, 0.2f, 0.0f);
    }

    //turns object
    void Turn()
    {
        transform.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime));
    }
     //changes the position based on chosen location
    void FollowChoice()
    {
        Vector3 newPos = new Vector3(santaManager.spawnPoints[santaManager.chosenSpawnpointIndex].position.x, transform.position.y, santaManager.spawnPoints[santaManager.chosenSpawnpointIndex].position.z);
        transform.position = newPos;
    }
}
