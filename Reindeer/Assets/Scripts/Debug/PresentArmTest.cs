using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentArmTest : MonoBehaviour
{
	public float HealAmount = 25.0f;
	public float DamageAmount = 5.0f;

    //launch vars
    public float verticalLaunchSpeed = 30.0f; //speed of upwards launch
    public float horizontalLaunchSpeed = 20.0f; //speed of horizontal push

    private float startY; //the starting y when spawning
    private float currentY; //current y of the 
    private bool hasLaunched = false; //check if has launched away from sled
    private bool isFalling = false; //check if present is falling after launch

    //arming vars
    public float armTime = 2.0f; //time taken to arm present
	public float adjustment = 3.0f; //adjustment to put present on ground

    private int presentType = 1; //type representation of present, 0 = heal, 1 = bomb
    private float startTime; //time that present spawned in
    private bool isArmed = false; //check if present is armed

    //physics vars
    private Rigidbody rigid; //reference to rigidbody component of this object
    //private BoxCollider collider; //reference to collider

    //particles variables
    public GameObject blastParticle; //ref to blast vfx
    public GameObject healParticle; //ref to heal vfx

    private ParticleSystem idleParticle; //ref to idle vfx

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        idleParticle = GetComponentInChildren<ParticleSystem>();
        //hasLaunched = true;
        isArmed = false;
        //set start time to now
        startTime = Time.time;
        //stop particle system from playing
        idleParticle.Pause();
        //shoot up
        LaunchForward();

    }

    // Update is called once per frame
    void Update()
    {
        //after launch check if falling yet
        if (hasLaunched)
        {
            CheckFalling();
        }
        //once falling check if reached ground level
        if (isFalling)
        {
            CheckLanded();
        }
    }

    void LaunchForward()
    {
        //make sure velocity is zero at start
        rigid.velocity = Vector3.zero;
        //get vector between forward and up
        Vector3 halfBetween = (transform.up + transform.forward).normalized;
        //get vector between half and up
        Vector3 launchDirection = (transform.up + halfBetween).normalized;
        //set velocity
        rigid.velocity = launchDirection * verticalLaunchSpeed;
        //add force in launch direction
        rigid.AddForce(Vector3.up, ForceMode.Impulse);
        //change has launched to true
        hasLaunched = true;
        //set start y
        startY = transform.position.y;
    }

    //sets up the bomb
    void ArmBomb()
    {
        //set bomb type
        presentType = Random.Range(0, 2);
        //play particle
        idleParticle.Play();
        //arm the bomb
        isArmed = true;
    }

    //check if rising after launch


    //checks if the present is currently falling
    void CheckFalling()
    {
        if (transform.position.y < currentY)
        {
            isFalling = true;
        }
        //else not falling, update currenty
        else
        {
            currentY = transform.position.y;
        }
    }

    //once falling, arm bomb when landing at starting y <- on ground
    void CheckLanded()
    {
        //if currenty is less than or equal to start y, has reached ground level
        if (currentY <= startY)
        {
            //set y to the start y
            transform.position = new Vector3(transform.position.x, startY + adjustment, transform.position.z);
            //freeze pos
            rigid.constraints = RigidbodyConstraints.FreezeAll;
            //arm the bomb
            ArmBomb();
        }
        //else update currenty against current pos
        else
        {
            currentY = transform.position.y;
        }
    }

    //when present is activated
    void Detonate()
    {
        if (presentType == 0)
        {
            //spawn heal particle
            GameObject healClone = healParticle;
            Instantiate(healClone, transform.position, transform.rotation);
            //do heal thing
        }
        else if (presentType == 1)
        {
            //spawn blast particle
            GameObject blastClone = blastParticle;
            Instantiate(blastClone, transform.position, transform.rotation);
            //do blast thing
        }
        //destroy this object
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        ////if colliding with arena, stop movement and arm the present
        //if (other.gameObject.CompareTag("Arena"))
        //{
        //    rigid.constraints = RigidbodyConstraints.FreezeAll;
        //    ArmBomb();
        //}
        //if colliding with a player, detonate the present
        if (other.gameObject.CompareTag("Player"))
        {
            print("Found player");
            if (isArmed)
            {
                print("Found armed bomb");
				if (presentType == 0) {
					other.gameObject.GetComponent<HealthManagement> ().IncreaseHealth (HealAmount);
				} else if (presentType == 1) {
					other.gameObject.GetComponent<HealthManagement>().DecreaseHealth(DamageAmount);
				}
                Detonate();
            }

        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //if colliding with arena, stop movement and arm the present
    //    if (collision.gameObject.CompareTag("Arena"))
    //    {
    //        //rigid.constraints = RigidbodyConstraints.FreezeAll;
    //        ArmBomb();
    //    }
    //    //if colliding with a player, detonate the present
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        //check if the present has armed
    //        if (isArmed)
    //        {
    //            Detonate();
    //        }

    //    }
    //}
}
