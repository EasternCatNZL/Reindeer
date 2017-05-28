using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class SledAttack : MonoBehaviour {

	//spin attack vars
	[Header("Attack Stats")]
	public float AttackDamage = 5.0f;
	private bool isAttacking = false; //checks if attacking
    private bool isSpinning = false; //checks if spinning

	//present attack vars
    [Header("Present Prefab")]
	public GameObject presentPrefab; //present prefab

	//animator stuff
	Animator anim;

    //particles
    [Header("Particles")]
    public GameObject presentSpawnParticle; //ref to particles played when present spawned
    public GameObject sledSpinImpactParticle; //ref to particle when sled spin hits something


    //Xinput stuff
    [Header("XInput Settings")]
	public PlayerIndex index;

	GamePadState state;

    //audio
    [Header("Audio")]
    public AudioSource sledImpactSound;
    public AudioSource presentThrowSound;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//check that not currently spinning
		if (!isAttacking) {
			state = GamePad.GetState(index);
			SpinAttack ();
            ThrowPresent();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		//if spin on
		if (isSpinning)
		{
            //kill things
			if (collision.gameObject.tag == "Reindeer") 
			{
				collision.gameObject.GetComponent<HealthManagement> ().DecreaseHealth (AttackDamage);
			}
            //get direction to other
            Vector3 directionVec = (transform.position - collision.gameObject.transform.position).normalized;
            //Quaternion direction = directionVec;
            //spawn a impact particle
            GameObject particleClone = sledSpinImpactParticle;
            Instantiate(particleClone, collision.transform.position, collision.transform.rotation);
            //vary pitch
            sledImpactSound.pitch = Random.Range(0.6f, 1.0f);
            //play sound
            sledImpactSound.Play();
        }
	}

	//picks a random direction and throws a present
	void ThrowPresent()
	{
		//get input to throw
		//state = GamePad.GetState(index);
		if (state.Buttons.X == ButtonState.Pressed && !isAttacking) {
            //set is attacking to true
            isAttacking = true;
            //fire anim
            anim.SetTrigger("Present");
		}
	}

	//anim call, spawn present during anim
	void SpawnPresent(){
		//get a random y rotation
		float randomYRotation = Random.Range(0, 360);
		//create new quaternion with random y rotation
		Quaternion alteredRotation = new Quaternion();
		alteredRotation.eulerAngles = new Vector3(0.0f, randomYRotation, 0);
		//create clone of object
		GameObject presentClone = presentPrefab;
		//spawn object
		Instantiate(presentClone, transform.position, alteredRotation);
        //create clone of particle
        GameObject particleClone = presentSpawnParticle;
        //spawn object
        Instantiate(particleClone, transform.position, alteredRotation);
        //vary pitch
        presentThrowSound.pitch = Random.Range(0.6f, 1.0f);
        //play sound
        presentThrowSound.Play();
    }

	//spin attack logic
	void SpinAttack()
	{
		
		//get input
		//state = GamePad.GetState(index);
		if (state.Triggers.Right >= 0.5f && !isAttacking) {
			//set attacking to true
			isAttacking = true;
			//fire animator
			anim.SetTrigger ("Spin");
		}
	}

	//anim call to end attack
	void EndAttack(){
		isAttacking = false;
	}

    //anim call to set spin on
    void SetSpinOn()
    {
        isSpinning = true;
    }

    //anim call to end spin
    void EndSpinAttack()
    {
        isSpinning = false;
		isAttacking = false;
    }
}