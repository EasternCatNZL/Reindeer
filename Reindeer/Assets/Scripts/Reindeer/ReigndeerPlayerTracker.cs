using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReigndeerPlayerTracker : MonoBehaviour {

	public Transform HeadRef;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = Vector3.zero;
		temp.x = HeadRef.position.x;
		temp.y = transform.position.y;
		temp.z = HeadRef.position.z;
		transform.position = temp;
		Quaternion newRotation = new Quaternion();
		newRotation.eulerAngles = new Vector3(0.0f, HeadRef.rotation.eulerAngles.y, 0);
		transform.rotation = newRotation;
	}
}
