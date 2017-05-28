﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class ShieldAttack : MonoBehaviour {

    //attack vars
    [Header("Attack Values")]
    public float attackDelay = 2.0f; //time that needs to past between attacks
    public ShieldWeapon weapon; //ref to weapon script

    private bool isAttacking = false; //check if currently attacking
    private float lastAttackTime; //time since last attack

    //shielding vars
    [Header("Shielding values")]
    public float shieldDelay = 5.0f; //time that needs to past between shields

    private bool isShielding = false; //checks if shielding
    private float lastShieldTime; //time of last shield

    //animator
    private Animator anim; //ref to animator

    //xinput vars
    [Header("XInput settings")]
    public PlayerIndex index;

    private GamePadState state;

    //particles
    [Header("Particles")]
    public GameObject exShieldStartParticle; //ref to particle emitted when special used

    //audio
    [Header("Audio")]
    public AudioSource shieldSound;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		//if other input not going
        if (!isAttacking && !isShielding)
        {
			state = GamePad.GetState(index);
			MeleeAttack ();
			ShieldSpecial ();
        }
	}

    //melee attack
    void MeleeAttack()
    {
        //get input
        //state = GamePad.GetState(index);
		if (state.Triggers.Right >= 0.5f && !isAttacking && !isShielding)
        {
            //set is attacking to true;
            isAttacking = true;
            //fire animator
            anim.SetTrigger("Attack");
        }
    }

    //shield
    void ShieldSpecial()
    {
        //get input
        //state = GamePad.GetState(index);
        if (state.Buttons.X == ButtonState.Pressed && !isAttacking && !isShielding)
        {
            //set shielding to true
            isShielding = true;
            GetComponent<HealthManagement>().Shielded = true;
            //fire animator
            anim.SetTrigger("Shield");
            //spawn particles
            GameObject particleClone = exShieldStartParticle;
            Instantiate(particleClone, transform.position, transform.rotation);
        }
    }

    //anim call for melee attacks
    void FirstSecondAttackActive()
    {
        weapon.isActive = true;
        weapon.isFirstSecondHit = true;
    }

    void EndFirstSecondAttack()
    {
        weapon.isActive = false;
        weapon.isFirstSecondHit = false;
    }

    void ThirdAttackActive()
    {
        weapon.isActive = true;
        weapon.isThirdHit = true;
    }

    void EndThirdAttack()
    {
        weapon.isActive = false;
        weapon.isThirdHit = false;
    }

    //anim call to end attack
    void EndAttack()
    {
        isAttacking = false;
        //weapon.isActive = false;
    }

    //anim call to end shield
    void EndShield()
    {
		isShielding = false;
        GetComponent<HealthManagement>().Shielded = false;
    }
}
