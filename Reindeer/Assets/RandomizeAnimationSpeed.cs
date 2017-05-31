using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimationSpeed : MonoBehaviour {

    public float MinSpeed = 0.0f;
    public float MaxSpeed = 1.0f;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Animator>().speed = Random.Range(MinSpeed, MaxSpeed);
    }
}
