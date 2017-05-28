using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    //player movement var
    public float moveSpeed; //speed at which player moves
    public float dashSpeed; //speed boost gained when player dashes
    public AudioSource dashSound; //audio ref to dash sound effect

    private bool m_bFalling = true; //checks to see if player is currently airbourne

    //player movement input var
    private float fdashCooldown = 3.0f; //time that needs to past between dashes
    private float fdashCooldownTimer; //time of last dash input
    private bool bcanDash = true; //checks to see if player can currently input dash command
    private Vector3 moveInput; //stores vector for movement input
    private Vector3 moveVelocity; //speed of movement for inputted directional vector

    //player body var
    private Rigidbody myRigidbody; //ref to rigidbody component

    //player weapon var
    public GunController theGun; //ref to player gun script

    //camera ref 
    private Camera mainCamera; //reference to the camera object

    //controller plugin vars
    bool playerIndexSet = false;
    //PlayerIndex playerIndex;
    //GamePadState state;
    //GamePadState prevState;

    // Use this for initialization
    void Start()
    {
        //get rigidbody component
        myRigidbody = GetComponent<Rigidbody>();
        //get camera ref
        mainCamera = FindObjectOfType<Camera>();

        //playerIndex = PlayerIndex.One;
    }

    // Update is called once per frame
    void Update()
    {
        //prevState = state;
        //state = GamePad.GetState(playerIndex);

        //check to see if player is currently grounded
        if (!m_bFalling)
        {
            MovePlayer();
        }

        //dash input
        PlayerDash();

        //direction input
        PlayerDirectionInput();

        //shooting
        PlayerShot();
    }

    void OnCollisionEnter(Collision _other)
    {
        //if made contact with arena, has reached the ground
        if (_other.gameObject.tag == "Arena")
        {
            m_bFalling = false;
        }
    }

    //used by others to push this object back using physics force
    public void PushBack(Vector3 _vDirection, float _fStrengh)
    {
        //set falling to true <- airbourne
        m_bFalling = true;
        //apply a force to launch object
        gameObject.GetComponent<Rigidbody>().AddForce(_vDirection, ForceMode.Impulse);
    }

    //player movement logic
    private void MovePlayer()
    {
        //get movement input from joystick
        //moveInput = new Vector3(-state.ThumbSticks.Left.Y, 0.0f, state.ThumbSticks.Left.X);
        //set movement in direction vector
        moveVelocity = moveInput * moveSpeed;
        //if not stationary, set animator to move
        if (moveVelocity != new Vector3(0.0f, 0.0f, 0.0f))
        {
            GetComponent<Animator>().SetBool("Running", true);
        }
        //else set to stationary
        else
        {
            GetComponent<Animator>().SetBool("Running", false);
        }
        //set rigidbody speed
        myRigidbody.velocity = moveVelocity;
    }

    //logic for player dash
    private void PlayerDash()
    {
        //check if cooldown time has passed
        if (fdashCooldownTimer <= Time.deltaTime + fdashCooldown)
        {
            //check if button has been pressed for dash input
			/*
            if((prevState.Buttons.A == ButtonState.Pressed || state.Buttons.A == ButtonState.Pressed) && bcanDash)
            {
                //move body forward by dash speed along direction vector
                myRigidbody.MovePosition(transform.position + moveInput * dashSpeed);
                //play death sound effect
                dashSound.Play();
                //set last time to current time
                fdashCooldownTimer = Time.time;
            }*/
        }
    }

    //sets players direction based on stick input
    private void PlayerDirectionInput()
    {
        //get direction from stick input
        Vector3 playerDirection = Vector3.forward /** state.ThumbSticks.Right.X*/ + Vector3.right /** -state.ThumbSticks.Right.Y*/;
        //set direction
        if (playerDirection.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
        }
    }

    //shotting logic
    private void PlayerShot()
    {
        //if trigger held then shoot
		/*
        if (state.Triggers.Right > 0.5f)
        {
            theGun.isFiring = true;
        }
        //else, don't shoot
        if (state.Triggers.Right < 0.5f)
        {
            theGun.isFiring = false;
        }
        */
    }
}
