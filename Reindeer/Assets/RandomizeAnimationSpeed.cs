using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimationSpeed : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Animator>().speed = Random.Range(0.8f, 1.2f);
    }
}
