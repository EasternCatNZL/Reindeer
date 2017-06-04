using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaChimneyBulletController : MonoBehaviour {

    //movement physics vars
    [Header("Physics vars")]
    public float moveSpeed = 2.0f; //speed at which bullet flies

    //bullet control vars
    [Header("Control vars")]
    public Transform destination; //destination bullet has to reach

    // Use this for initialization
    void Start () {
        destination = GameObject.Find("[CameraRig]/Camera (head)/Camera (eye)").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, destination.position, moveSpeed * Time.deltaTime);
	}


}
