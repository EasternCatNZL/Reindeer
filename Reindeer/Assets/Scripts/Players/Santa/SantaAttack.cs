using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class SantaAttack : MonoBehaviour
{

	public GameObject CooldownIcon = null;
    //basic attacking vars
    //public bool isFiring; //checks if player is currently attacking
    [Header("Attack values")]
    public float shotDelay; //time that needs to past between shots

    private float lastShotTime; //time of last shot
	private bool isAttacking = false; //checks if attacking

    //special attack vars
    [Header("Special attack values")]
    public float specialDelay; //time that needs to past between specials
	public GameObject SplatImage;

    private float lastSpecialTime; //time of last special
	private bool SpecialReady = true;

    //transform ref
    [Header("Transforms for Projectiles")]
    public Transform firePoint; //ref to transform at which to spawn attack
    public Transform chimneyPoint; //ref to chimney transform
	private Transform firePointClone;
	private Transform chimneyPointClone;

    //animator
    Animator anim; //ref to animator

    //bullet prefab
    [Header("Bullet prefabs")]
    public GameObject santaBulletPrefab;
    public GameObject santaChimneyBulletPrefab;

    //particles
    [Header("Particles")]
    public GameObject santaShotCastParticle; //ref to particle emitted from gun when shooting <- not the bullet
    public GameObject santaChimneyCastParticle; //ref to particle emitted from chimney <- not the bullet
    public GameObject santaShotBulletParticle; //ref to the particle bullet shot
    public GameObject santaChimneyBulletParticle; //ref to the chimeny shot particle

    //xinput stuff
    public PlayerIndex index;

    GamePadState state;

    //audio
    [Header("Audio")]
    public AudioSource santaShotSound;
    public AudioSource santaChimneyShotSound;
    public AudioSource splatSound;


    // Use this for initialization
    void Start()
	{
        anim = GetComponent<Animator>();
		firePointClone = firePoint;
		chimneyPointClone = chimneyPoint;
		if(CooldownIcon)CooldownIcon.GetComponent<UICooldownIcons> ().SetReady ();
    }

    // Update is called once per frame
    void Update()
    {
		if (Time.time - lastSpecialTime > specialDelay) {
			SpecialReady = true;
			if(CooldownIcon)CooldownIcon.GetComponent<UICooldownIcons> ().SetReady ();
		}
        if (!isAttacking)
        {
			state = GamePad.GetState(index);
            FireShot();
            SpecialShot();
        }
    }

    //fires bullets
    private void FireShot()
    {
        //if cooldown time has passed
        if (lastShotTime <= Time.time + shotDelay)
        {
            //if input
            //state = GamePad.GetState(index);
			if (state.Triggers.Right >= 0.5f)
            {
				print("Pressed a");
                //set last shot time to current time
                lastShotTime = Time.time;
                //fire anim
                anim.SetTrigger("Shooting");
                //set is attacking
                isAttacking = true;
            }
        }
    }

    //fires special attack
    private void SpecialShot()
    {
        //if cooldown time has passed
        //state = GamePad.GetState(index);
		if (SpecialReady)
        {
            //if input
            if (state.Buttons.X == ButtonState.Pressed)
            {
                //do special stuff
                anim.SetTrigger("Chimney");
                //set is attacking
                isAttacking = true;
				SpecialReady = false;
				if(CooldownIcon)CooldownIcon.GetComponent<UICooldownIcons> ().SetGrey ();
				GetComponent<BasicMovement> ().SetDisabled (true);
				lastSpecialTime = Time.time;
            }

        }
    }

    //anim call to spawn bullet
    void ShootGun()
    {
        //create a new bullet
		//print(firePoint);
		//print (firePointClone);
        GameObject bulletClone = santaBulletPrefab;
		Instantiate(bulletClone, firePointClone.position + new Vector3(0.0f, 0.1f, 0.0f), firePointClone.rotation);
        //vary pitch
        santaShotSound.pitch = Random.Range(0.6f, 1.0f);
        //play sound
        santaShotSound.Play();
    }

    //anim call to shoot chimney attack
    void ChimneyShot()
    {
        //create a new bullet
        GameObject bulletClone = santaChimneyBulletPrefab;
		Instantiate(bulletClone, chimneyPointClone.transform.position, chimneyPointClone.transform.rotation);
        //stop movement while anim playing
		GetComponent<BasicMovement> ().SetDisabled (false);
		//SplatImage.GetComponent<ScreenSplat>().ResetAlpha();
        //vary pitch
        santaChimneyShotSound.pitch = Random.Range(0.6f, 1.0f);
        //play sound
        santaChimneyShotSound.Play();
        //vary pitch
        splatSound.pitch = Random.Range(0.6f, 1.0f);
        //play sound
        splatSound.Play();
    }

    //anim call to end attack 
    void EndAttack()
    {
        isAttacking = false;
    }
}