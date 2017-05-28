using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatMiniReindeer : MonoBehaviour {

    public AudioSource EatSound;
    public GameObject Reigndeer = null;
    public float HealAmount = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Mini") 
		{
			Destroy (other.gameObject);
            if(Reigndeer)
            {
                Reigndeer.GetComponent<HealthManagement>().IncreaseHealth(HealAmount);
				Reigndeer.GetComponent<Animator> ().SetTrigger ("Eating");
                EatSound.Play();
            }
		}
	}
}
