using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class BasicMovement : MonoBehaviour
{
    //PUBLIC

	//Basic Movement
	[Header("Basic Settings")]
	public bool DiagonalMovement = true;
	public float Speed = 2;
	public Transform AlignMovement = null;
    //Camera
    [Header("Camera Settings")]
    public Camera TrackCamera = null;
	public GameObject CameraTarget = null;
    public float CameraHeight = 5.0f;
    public float CameraDistance = 5.0f;
	//Player Controller
	[Header("Controller Settings")]
	public PlayerIndex PlayerNumber;
	public float DeadzoneX = 0.0f;
	public float DeadzoneY = 0.0f;
	public float AimDeadzoneX = 0.0f;
	public float AimDeadzoneY = 0.0f;
    //Strafing 
    [Header("Strafe Settings")]
    public bool Strafe = false;
	//Particles
	[Header("Particles")]
	public ParticleSystem movingParticles;
    

    //PRIVATE
	//Controller members
	private GamePadState state;
    //BasicMovement
	private bool Disabled = false;
    private Vector3 ZeroVector = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 MovementVector = new Vector3(0.0f, 0.0f, 0.0f);

    //Strafing
    private Vector3 StrafeForward = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 StrafeRight = new Vector3(0.0f, 0.0f, 0.0f);

	//Animators
	private Animator anim;

    // Use this for initialization
    void Start()
    {
		GetComponent<Rigidbody> ().freezeRotation = true;
        if (!AlignMovement)
        {
            AlignMovement = transform;
        }
		if (CameraTarget && TrackCamera) 
		{
			TrackCamera.GetComponent<TrackingCamera> ().SetTarget (CameraTarget);
			TrackCamera.GetComponent<TrackingCamera> ().Init (transform, CameraHeight, CameraDistance);
		} 

		anim = GetComponent<Animator> ();

		movingParticles.Pause ();
    }

    // Update is called once per frame
    void Update()
    {
		if (!Disabled) 
		{
			state = GamePad.GetState (PlayerNumber);
			Movement ();
			if (Strafe && CameraTarget) {
				StrafeMovement ();
				Vector3 CorrectedTransform = new Vector3 (CameraTarget.transform.position.x, transform.position.y, CameraTarget.transform.position.z);
				transform.LookAt (CorrectedTransform);
			} else {
				RotatePlayer ();
			}
			Vector3 backwardsDirection = transform.position - (transform.position + transform.forward);
			movingParticles.transform.LookAt (backwardsDirection);
		}
    }

    private void FixedUpdate()
	{
		if (!Disabled) 
		{
			this.GetComponent<Rigidbody> ().MovePosition (transform.position + MovementVector);
		}
    }

    private void Movement()
    {
        MovementVector = ZeroVector;
        if (DiagonalMovement)
        {
			if (Input.GetKey(KeyCode.W) || state.ThumbSticks.Left.Y > DeadzoneY)
            {
                MovementVector = MovementVector + AlignMovement.forward * Time.deltaTime * Speed;
            }
			if (Input.GetKey(KeyCode.S) || state.ThumbSticks.Left.Y < DeadzoneY)
            {
                MovementVector = MovementVector + -AlignMovement.forward * Time.deltaTime * Speed;
            }
			if (Input.GetKey(KeyCode.A) || state.ThumbSticks.Left.X < DeadzoneX)
            {
                MovementVector = MovementVector + -AlignMovement.right * Time.deltaTime * Speed;
            }
			if (Input.GetKey(KeyCode.D) || state.ThumbSticks.Left.X > DeadzoneX)
            {
                MovementVector = MovementVector + AlignMovement.right * Time.deltaTime * Speed;
            }
        }
        else
        {
			if (Input.GetKey(KeyCode.W) || state.ThumbSticks.Left.Y > DeadzoneY)
            {
                MovementVector = MovementVector + AlignMovement.forward * Time.deltaTime * Speed;
            }
			else if (Input.GetKey(KeyCode.S) || state.ThumbSticks.Left.Y < DeadzoneY)
            {
                MovementVector = MovementVector + -AlignMovement.forward * Time.deltaTime * Speed;
            }
			else if (Input.GetKey(KeyCode.A) || state.ThumbSticks.Left.X < DeadzoneX)
            {
                MovementVector = MovementVector + -AlignMovement.right * Time.deltaTime * Speed;
            }
			else if (Input.GetKey(KeyCode.D) || state.ThumbSticks.Left.X > DeadzoneX)
            {
                MovementVector = MovementVector + AlignMovement.right * Time.deltaTime * Speed;
            }
        }

		//if no movement
		if (MovementVector == Vector3.zero) {
			anim.SetBool ("Moving", false);
			movingParticles.Stop ();
		} else {
			anim.SetBool ("Moving", true);
			movingParticles.Play ();
		}

    }

    private void StrafeMovement()
    {
        MovementVector = ZeroVector;
		Vector3 StrafePositon = CameraTarget.transform.position;
        //Strafing Forward Vector
        StrafeForward.Set(StrafePositon.x - transform.position.x, 0.0f , StrafePositon.z - transform.position.z);
        StrafeForward.Normalize();
        //Strafing Right Vector
        StrafeRight = Vector3.Cross(StrafeForward, transform.up);
        StrafeRight.Normalize();
        if (DiagonalMovement)
        {
			if (Input.GetKey(KeyCode.W) || state.ThumbSticks.Left.Y > DeadzoneY)
            {
                MovementVector = MovementVector + StrafeForward * Time.deltaTime * Speed;
            }
			if (Input.GetKey(KeyCode.S) || state.ThumbSticks.Left.Y < DeadzoneY)
            {
                MovementVector = MovementVector + -StrafeForward * Time.deltaTime * Speed;
            }
			if (Input.GetKey(KeyCode.A) || state.ThumbSticks.Left.X < DeadzoneX)
            {
                MovementVector = MovementVector + StrafeRight * Time.deltaTime * Speed;
            }
			if (Input.GetKey(KeyCode.D) || state.ThumbSticks.Left.X > DeadzoneX)
            {
                MovementVector = MovementVector + -StrafeRight * Time.deltaTime * Speed;
            }
        }
    }

	private void RotatePlayer()
	{
		//get direction from stick input
		//print (state.ThumbSticks.Right.X + " : " + state.ThumbSticks.Right.Y);
		//set direction
		if (state.ThumbSticks.Right.X > AimDeadzoneX || state.ThumbSticks.Right.X < -AimDeadzoneX ||state.ThumbSticks.Right.Y > AimDeadzoneY || state.ThumbSticks.Right.Y < -AimDeadzoneY) 
		{
			Vector3 playerDirection = Vector3.zero;
			if (state.ThumbSticks.Right.X > AimDeadzoneX || state.ThumbSticks.Right.X < -AimDeadzoneX) {
				playerDirection += Vector3.forward * state.ThumbSticks.Right.X;
			}
			if (state.ThumbSticks.Right.Y > AimDeadzoneY || state.ThumbSticks.Right.Y < -AimDeadzoneY) {
				playerDirection += Vector3.right * -state.ThumbSticks.Right.Y;
			}
			transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
		}

	}

	public void SetDisabled(bool _NewState)
	{
		Disabled = _NewState;
	}
	public void SetPlayer(PlayerIndex _NewPlayerNumber)
	{
		PlayerNumber = _NewPlayerNumber;
	}


}
