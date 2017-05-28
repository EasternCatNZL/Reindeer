using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttackTest : MonoBehaviour {

    //weapon speed var
    public float swingSpeed; //speed of swing <- if moving weapon

    //basic attack vars
    public bool isAttacking = false; //check if currently attacking
    public float attackDelay = 2.0f; //time that needs to past between attacks

    private float lastAttackTime; //time since last attack

    //weapon var
    public ShieldWeapon weapon; //ref to weapon
    //public GameObject weapon; 

    //weapon pos var
    public Transform weaponSpawnPoint; //starting transform for the weapon

    //shielding var
    public bool isShielding = false; //check if currently shielding
    public float shieldMaxDuration = 4.0f; //time shield can be held for
    public float shieldChangeRate = 0.5f; //rate shield charges/decays
    public float shieldRechargeRate = 0.2f; //rate shield recharges after getting locked

    private bool canUseShield = true; //checks if can use shield
    private bool isShieldLocked = false; //check to see if shield is locked
    private float shieldLeft = 4.0f; //amount of shield left
    

    //script ref
    private GenericPlayerControlTest playerController; //ref to player control

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if not attacking
        if (!isAttacking)
        {
            //if shield is not locked,  allow shielding
            if (!isShieldLocked)
            {
                ToggleShield();
            }
            //if also not shielding, allow attack
            if (!isShielding)
            {
                MeleeSwing();
            }
        }
        //track shield duration every frame
        TrackShieldDuration();
        //if shield locked
        if (isShieldLocked)
        {
            //recharge the shield slowly
            RechargeShield();
        }
	}

    //attacks using melee hit
    private void MeleeSwing()
    {
        //if input do stuff
        //if cooldown time has passed
        if (lastAttackTime <= Time.time + attackDelay)
        {
            //set attacking to true
            isAttacking = true;
            //create new weapon instance
            ShieldWeapon newWeapon = Instantiate(weapon, weaponSpawnPoint.transform.position, weaponSpawnPoint.transform.rotation) as ShieldWeapon;
            //ref back to owner
            //newWeapon.owner = this;
            //set last attack time to current
            lastAttackTime = Time.time;
        }
    }

    //hold shield up
    private void ToggleShield()
    {
        //if input do stuff
        //if shield currently up
        if (isShielding)
        {
            //toggle off
            isShielding = false;
        }
        else
        {
            //if shield can be used
            if (canUseShield)
            {
                //toggle shield on
                isShielding = true;
            }
        }
    }

    //track time shield has been held
    private void TrackShieldDuration()
    {
        //if shield is up, reduce duration left
        if (isShielding)
        {
            shieldLeft -= shieldChangeRate * Time.deltaTime;
            //if out of shield
            if (shieldLeft <= 0)
            {
                //lock off the shield
                isShieldLocked = true;
                //stop shielding
                isShielding = false;
            }
        }
        //else recharge shield
        else
        {
            shieldLeft += shieldChangeRate * Time.deltaTime;
            //make sure not over max
            if (shieldLeft > shieldMaxDuration)
            {
                shieldLeft = shieldMaxDuration;
            }
        }
    }

    //recharge shield
    private void RechargeShield()
    {
        //recharge the shield
        shieldLeft += shieldRechargeRate * Time.deltaTime;
        //if shield full, turn off lock
        if (shieldLeft >= shieldMaxDuration)
        {
            isShieldLocked = false;
        }
    }
}
