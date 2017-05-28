using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseProjectile : MonoBehaviour {

    public float speed; //speed at which object moves
    public AudioSource noseImpact; //sound of nose impact

    private Transform Reindeer; //reference to transform of reindeer
    //private float DeltaTime;

    // Use this for initialization
    void Start () {
        Reindeer = GameObject.FindGameObjectWithTag("Reindeer").transform;
	}
	
	// Update is called once per frame
	void Update () {
        //float DeltaTime = Time.deltaTime;
        //move the object forward each frame
        transform.position += transform.forward * speed * Time.deltaTime;
	}

    void OnCollisionEnter(Collision _other)
    {
        //if colliding with the arena wall
        if (_other.gameObject.tag == "Arena")
        {
            //noseImpact.Play();
            //play audio at point of impact
            AudioSource.PlayClipAtPoint(noseImpact.clip, transform.position);
            Destroy(gameObject);
        }
        //if colliding with santa
        else if (_other.gameObject.tag == "Santa")
        {
            //kill santa
            _other.gameObject.GetComponent<SantaKill>().KillSanta();
        }
    }
}
