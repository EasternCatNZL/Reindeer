using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    //movement speed vars
    public float bulletSpeed; //speed of projectile

    //attacking vars
    public bool isFiring; //checks if player is currently attacking
    public float timeBetweenShots; //time that needs to past between shots

    private float lastShotTime; //time of last shot

    //object ref
    public BulletController bullet; //reference to bullet object
    
    //transform ref
    public Transform firePoint; //ref to transform at which to spawn attack

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if fire trigger is currently inputted
        if(isFiring == true)
        {
            FireShot();
        }
	}

    //fires bullets
    private void FireShot()
    {
        //if cooldown time has passed
        if (lastShotTime <= Time.deltaTime + timeBetweenShots)
        {
            //create a new bullet
            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
            //set speed of bullet instance
            newBullet.speed = bulletSpeed;
            //set last shot time to current time
            lastShotTime = Time.time;
        }
    }
}
