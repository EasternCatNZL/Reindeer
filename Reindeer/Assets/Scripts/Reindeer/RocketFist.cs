using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFist : MonoBehaviour
{

    //Public Members
    public GameObject ControllerRef;
	[Header("Player Refs")]
	public GameObject[] Players = new GameObject[3];
    [Header("Rocket Punch Settings")]
    public float LaunchSpeed = 25.0f;
    public float ReturnCooldown = 1.0f;
    public float ReturnSpeed = 20.0f;
    public float FlightTimeLimit = 1.5f; //How long the the hand can fly for
    public float RotationOffset = 0.0f; //Offset for the rocket fists
	public float HitDamage = 25.0f;
	public float HitRadius = 0.5f;
	public float KnockBackForce = 10.0f; //force of knock back impact
	public float KnockBackHeight = 5.0f;
    [Header("VFX Settings")]
    public GameObject ImpactVFX = null;
    public GameObject ImpactRocks = null;
    [Header("Audio")]
    public AudioSource punchLaunchSound;
    public AudioSource punchImpactSound;

    //Private Members
    private Rigidbody Rigid; //Reference to the GameObject's Rigibody

    public bool TrackHands = true;
    private bool OnReturnCooldown = false;
    private bool Returning = false; //State when the fist is returning
    private float ImpactTime; //When the fist hit the ground
    private float FlightTime = 0.0f; //Timer for flight length
    private Quaternion QuaternionOffset; //Quaternion representation of the rocket fist offset
    private Quaternion HandRotation;


    // Use this for initialization
    void Start()
    {
        if (!ControllerRef) Debug.LogError("No controller has been assigned to this hand, ", gameObject);
        Rigid = GetComponent<Rigidbody>();
        QuaternionOffset = new Quaternion();
        QuaternionOffset.eulerAngles = new Vector3(RotationOffset, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

        if (OnReturnCooldown) //Check if the Cooldown to return has expired
        {
            if (Time.time - ImpactTime > ReturnCooldown)
            {
                Returning = true;
                Rigid.constraints = RigidbodyConstraints.None;
            }
        }
        else if (!OnReturnCooldown && !TrackHands && Time.time - FlightTime > FlightTimeLimit) //Check to make sure the hand doesn't fly away
        {
            ImpactTime = Time.time;
            OnReturnCooldown = true;
            Rigid.velocity = Vector3.zero;
        }
        if (Returning)
        { //Return the hand to sender
            Vector3 temp = ControllerRef.transform.position - transform.position;
            if (temp.magnitude < 0.1f)
            {//Check the fist has returned
                transform.position = ControllerRef.transform.position;
                Returning = false;
                OnReturnCooldown = false;
                TrackHands = true;
            }

            temp.Normalize();
            temp *= ReturnSpeed * Time.deltaTime;

            Rigid.MovePosition(transform.position + temp);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Launch();
        }
    }

	void FixedUpdate()
	{
		if (TrackHands)
		{
			transform.position = ControllerRef.transform.position;
			transform.rotation = ControllerRef.transform.rotation;
		}
	}

    //void OnCollisionEnter(Collision _other)
    //{
    //    Rigid.constraints = RigidbodyConstraints.FreezeAll;
    //    ImpactTime = Time.time;
    //    OnReturnCooldown = true;
    //}

    void OnTriggerEnter(Collider other)
    {
        if (!OnReturnCooldown)
        {
			if (other.tag == "Arena" || other.tag == "Player")
            {
                Instantiate(ImpactVFX, transform.position, ImpactVFX.transform.rotation);
                Instantiate(ImpactRocks, transform.position, transform.rotation);

                //vary pitch
                punchImpactSound.pitch = Random.Range(0.6f, 1.0f);
                //play sound
                punchImpactSound.Play();

                foreach (GameObject _Player in Players)
				{
					
					Vector3 _Distance = _Player.transform.position - transform.position;
					if(_Distance.magnitude < HitRadius)
					{
						_Player.GetComponent<Rigidbody>().AddForce(((_Player.transform.position - transform.position) * KnockBackForce) + new Vector3(0.0f, KnockBackHeight, 0.0f), ForceMode.Impulse);
						_Player.GetComponent<HealthManagement> ().DecreaseHealth (HitDamage);
                        print("Hit Players");
					}
				}

                Rigid.constraints = RigidbodyConstraints.FreezeAll;
                ImpactTime = Time.time;
                OnReturnCooldown = true;
            }
        }
    }

    public void Launch()
    {
        if (!OnReturnCooldown && TrackHands)
        {
            FlightTime = Time.time;
            TrackHands = false;

            Rigid.AddForce(ControllerRef.transform.forward.normalized * LaunchSpeed, ForceMode.Impulse);

            //vary pitch
            punchLaunchSound.pitch = Random.Range(0.6f, 1.0f);
            //play sound
            punchLaunchSound.Play();
        }
    }
}
