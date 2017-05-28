using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPlayerControlTest : MonoBehaviour {

    //player movement var
    public float moveSpeed; //speed at which player moves

    private bool m_bFalling = true; //checks to see if player is currently airbourne

    //player movement input var
    private Vector3 moveInput; //stores vector for movement input
    private Vector3 moveVelocity; //speed of movement for inputted directional vector

    //player body var
    private Rigidbody myRigidbody; //ref to rigidbody component

    //camera ref 
    private Camera mainCamera; //reference to the camera object

    //damage interaction
    public bool canTakeDamage = true; //checks for can take damage

    //controller plugin vars
    public int playerIndex = 0; //which controller is this player connected to

    bool playerIndexSet = false;
    //PlayerIndex playerIndex;
    //GamePadState state;
    //GamePadState prevState;

    // Use this for initialization
    void Start () {
        //get rigidbody component
        myRigidbody = GetComponent<Rigidbody>();
        //get camera ref
        mainCamera = FindObjectOfType<Camera>();
    }
	
	// Update is called once per frame
	void Update () {

        //check to see if player is currently grounded
        if (!m_bFalling)
        {
            MovePlayer();
        }

        //direction input
        PlayerDirectionInput();

    }

    void OnCollisionEnter(Collision _other)
    {
        //if made contact with arena, has reached the ground
        if (_other.gameObject.tag == "Arena")
        {
            m_bFalling = false;
        }
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
}
