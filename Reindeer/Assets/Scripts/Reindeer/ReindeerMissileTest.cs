using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReindeerMissileTest : MonoBehaviour {

	private bool NoPlayers = false;
    public GameObject MissileAbilityRef;
    //launch vars
    public float launchSpeed = 50.0f; //speed of initial launch

    //targeting vars
    public float acceleration = 1.0f; //additional force added to object
    public float rotationAdjustSpeed = 0.5f; //time used to adjust rotation in flight
    public float flightAdjustDelay = 0.5f; //time between adjustments
    public GameObject[] players = new GameObject[3]; //array of player characters

    private float currentSpeed = 0.0f;
    private float maxSpeed = 3.0f; //maximum speed that the object can reach
    private float lastAdjustment = 0.0f; //time of last adjustment
    private bool airbourne = false; //checks to see if missile has been launched
    private bool targetAquired = false; //checks to see if a target exists
    private GameObject targetPlayer; //ref to targeted player
    public Vector3 targetPosition; //Target position

    //physics vars
    private Rigidbody rigid; //ref to rigidbody component on this object

    //logic control vars
    public float delayBeforeTracking = 3.0f; //delay before missle locks on

    //Damage stats
    [Header("Missile Damage Stats")]
    public float ExplosionDamage = 5.0f;
    public float ExplosionRadius = 1.0f;
    public float KnockbackForce = 1.0f;
    public float KnockbackHeight = 2.0f;

    //VFX
    public GameObject ExplosionVFX;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.FreezeAll;
		MissileAbilityRef = GameObject.FindGameObjectWithTag ("Reindeer");
        players = GameObject.FindGameObjectsWithTag("Player");
		if (players.Length == 0) {
			NoPlayers = true;
		}
        //Launch();
    }
	
	// Update is called once per frame
	void Update () {
		if (targetAquired)
        {
            AdjustRotation();
        }
        if (airbourne)
        {
            if (Time.time <= lastAdjustment + flightAdjustDelay)
            {
                Readjust();
            }
        }
	}

    //initial launch force
    public void Launch()
    {
        transform.rotation = Random.rotation;
		rigid.velocity = Vector3.zero;
        rigid.constraints = RigidbodyConstraints.None;

        //add velocity
        rigid.velocity = Vector3.up * launchSpeed;
        //apply the force
        rigid.AddForce(Vector3.up, ForceMode.Impulse);
        //begin tracking target
        Invoke("BeginTracking", delayBeforeTracking);
    }

    //iterates through players and finds closest player at search time
    void FindTarget()
    {
        //set min distance to infinity
        float minDist = Mathf.Infinity;
        //for all players in array
        for (int i = 0; i < players.Length; i++)
        {
            //if player exists
            if (players[i])
            {
                //check to see if this players position is closer than current min distance
                float thisDist = Vector3.Distance(players[i].transform.position, transform.position);
                if (thisDist < minDist)
                {
                    //set current player to target
                    targetPlayer = players[i];
                    targetAquired = true;
                }
            }
        }
    }

    //moves towards target
    void FlyAtTarget()
    {
        //set airbourne to true
        airbourne = true;
        //remove the initial launch force
        rigid.velocity = Vector3.zero;
        //set up current speed
        currentSpeed += acceleration * Time.deltaTime;
        //add force
        rigid.velocity = transform.forward * currentSpeed;
        rigid.AddForce(transform.forward, ForceMode.VelocityChange);
        //set time of last adjustment to now
        lastAdjustment = Time.time;
    }

    //correct the course of flight
    void Readjust()
    {
        //set up current speed
        currentSpeed += acceleration * Time.deltaTime;
        //remove the initial launch force
        rigid.velocity = Vector3.zero;
        //check if reached max speed
        if (rigid.velocity.magnitude >= maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        //add force
        rigid.velocity += transform.forward * currentSpeed;
        rigid.AddForce(transform.forward, ForceMode.VelocityChange);
        //set time of last adjustment to now
        lastAdjustment = Time.time;
    }

    //functions to start tracking target
    void BeginTracking()
    {
        //FindTarget();
        targetAquired = true;
        FlyAtTarget();
    }

    //constantly adjusts rotation
    void AdjustRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.03f);
    }

    //Setters
    public void SetTargetPosition(Vector3 _newVector)
    {
        targetPosition = _newVector;

    }
    public void SetPosition(Vector3 _newPosition)
    {
        transform.position = _newPosition;
    }

    void OnCollisionEnter(Collision other)
    {
        Instantiate(ExplosionVFX, transform.position, ExplosionVFX.transform.rotation);

		if (!NoPlayers) {
			foreach (GameObject _Player in players) {
				Vector3 _Distance = _Player.transform.position - transform.position;
				if (_Distance.magnitude < ExplosionRadius) {
					_Player.GetComponent<Rigidbody> ().AddForce (((_Player.transform.position - transform.position) * KnockbackForce) + new Vector3 (0.0f, KnockbackHeight, 0.0f), ForceMode.Impulse);
					_Player.GetComponent<HealthManagement> ().DecreaseHealth (ExplosionDamage);
				}
			}
		}

		MissileAbilityRef.GetComponent<MissileAbility>().ResetTargets();

        targetAquired = false;
        airbourne = false;

        currentSpeed = 0.0f;

        rigid.constraints = RigidbodyConstraints.FreezeAll;
        SetPosition(new Vector3(0.0f, -20.0f, 0.0f));
    }
}
