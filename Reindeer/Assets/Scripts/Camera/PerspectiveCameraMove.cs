using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveCameraMove : MonoBehaviour {

    //camera
    [Header("Camera Ref")]
    public Camera playerCamera;

    //camera mov vars
    [Header("Camera Control Variables")]
    public float zoomFactor = 1.5f;
    public float followSpeed = 0.8f;
    public float snapDistance = 0.05f;

    private float smallX;
    private float bigX;
    private float smallZ;
    private float bigZ;

    //players
    [Header("Players")]
    public Transform[] players;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FixedCameraFollowSmooth();
	}

    private void FixedUpdate()
    {
        //FixedCameraFollowSmooth();
    }

    public void FixedCameraFollowSmooth()
    {
        //reset far distance values
        smallX = 20f;
        bigX = -20f;
        smallZ = 20f;
        bigZ = -20f;

        // Midpoint we're after
        //Get midpoint from average of all transforms tracked
        Vector3 midpoint = Vector3.zero;
        //print("Mid point before calc" + midpoint);
        for (int i = 0; i < players.Length; i++)
        {
            midpoint += players[i].position;
            //for each, check the x and z of the players and find furthest x,z on two sides
            if (players[i].position.x < smallX)
            {
                smallX = players[i].position.x;
                print("small x changed");
            }
            if (players[i].position.z < smallZ)
            {
                smallZ = players[i].position.z;
                print("small z changed");
            }
            if (players[i].position.x > bigX)
            {
                bigX = players[i].position.x;
                print("big x changed");
            }
            if (players[i].position.z > bigZ)
            {
                bigZ = players[i].position.z;
                print("big z changed");
            }
        }
        //print("Mid point After adding" + midpoint);
        //divide midpoint by number of players
        midpoint = midpoint / players.Length;
        //print("Mid point after averaging" + midpoint);

        //create a distance between x most and z most pos
        Vector3 vecA = new Vector3(smallX, 1.0f, smallZ);
        Vector3 vecB = new Vector3(bigX, 1.0f, bigZ);
        float distance = (vecA - vecB).magnitude;
        //print("Distance between " + distance);

        // Move camera a certain distance
        Vector3 cameraDestination = midpoint - playerCamera.transform.forward * distance * zoomFactor;
        //print("Camera destination" + cameraDestination);

        // You specified to use MoveTowards instead of Slerp
        playerCamera.transform.position = Vector3.Slerp(playerCamera.transform.position, cameraDestination, followSpeed);

        // Snap when close enough to prevent annoying slerp behavior
        if ((cameraDestination - playerCamera.transform.position).magnitude <= snapDistance)
            playerCamera.transform.position = cameraDestination;
    }
}
