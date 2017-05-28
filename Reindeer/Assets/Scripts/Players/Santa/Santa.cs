using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PushBack(Vector3 _vDirection, float _fStrengh)
    {
        Debug.DrawLine(transform.position, _vDirection);
        gameObject.GetComponent<Rigidbody>().AddForce(_vDirection, ForceMode.Impulse);
    }
}
