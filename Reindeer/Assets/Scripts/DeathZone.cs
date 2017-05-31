using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<HealthManagement> ().DecreaseHealth (1000.0f);
		} else if(other.gameObject.tag != "ReindeerHand" && other.gameObject.tag != "Ability"){
			Destroy (other.gameObject);
		}
    }
}
