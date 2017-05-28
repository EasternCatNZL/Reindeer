using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour {

    public Transform ObjectToTrack = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(ObjectToTrack)
        {
            transform.position = ObjectToTrack.position;
            transform.rotation = ObjectToTrack.rotation;
        }
	}
}
