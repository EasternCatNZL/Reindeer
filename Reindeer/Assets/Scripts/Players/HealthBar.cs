using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public Transform Reigndeer = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 Direction = new Vector3(Reigndeer.position.x, transform.position.y, Reigndeer.position.z);
		transform.LookAt (Direction);
	}
}