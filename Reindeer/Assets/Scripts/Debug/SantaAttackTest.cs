using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaAttackTest : MonoBehaviour {

    //movement speed vars
    public float bulletSpeed; //speed of projectile

    //basic attacking vars
    //public bool isFiring; //checks if player is currently attacking
    public float shotDelay; //time that needs to past between shots

    private float lastShotTime; //time of last shot

    //special attack vars
    public float specialDelay; //time that needs to past between specials

    private float lastSpecialTime; //time of last special

    //object ref
    public BulletController bullet; //reference to bullet object

    //transform ref
    public Transform firePoint; //ref to transform at which to spawn attack

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FireShot();
        SpecialShot();
	}

    //fires bullets
    private void FireShot()
    {
        //if cooldown time has passed
        if (lastShotTime <= Time.time + shotDelay)
        {
            //create a new bullet
            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
            //set speed of bullet instance
            newBullet.speed = bulletSpeed;
            //set last shot time to current time
            lastShotTime = Time.time;
        }
    }

    //fires special attack
    private void SpecialShot()
    {
        //if cooldown time has passed
        if (lastSpecialTime <= Time.time + specialDelay)
        {
            //do special stuff
        }
    }
}
