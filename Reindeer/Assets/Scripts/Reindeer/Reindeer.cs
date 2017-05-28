using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using XInputDotNetPure;
using UnityEngine.SceneManagement;

public class Reindeer : MonoBehaviour
{
    //player controller ref
    public PlayerController player; //reference to playercontroller script

    //reindeer slam attack var
    public float slamStrength = 5; //distance player gets launched by slam
    public float slamHeight = 5; //height player rises from slam

    //reindeer health var
    public float maxHeight = 1000; //health of the reindeer
    public Image healthBar; //reference to health bar image
    public AudioSource deathAudio; //audio reference to reindeer death sound effect

    private float health = 0; //current health of the reindeer
    private bool alive = true; //check for whether reindeer is alive

    //reindeer attack var
    private float attackInputCooldown = 1.0f; //time between inputs 
    private float timeSinceLastAttack = 0; //holds time of last input

    //reindeer nose attack var
    public GameObject noseProjectile; //reference to noseLuanchAudio projectile
    public AudioSource noseLuanchAudio; //audio reference to reindeer nose shot sound effect

    //reindeer slam attack var
    public ParticleSystem slamVFX; //reference to shockwave particle
    public AudioSource slamAudio; //audio reference to slam sound effect
    public AudioSource jumpAudio; //audio reference to reindeer jump sound effect

    //controller plugin vars
    private bool playerIndexSet = false;
    //private PlayerIndex playerIndex;
    //private GamePadState state;
    //private GamePadState prevState;

    // Use this for initialization
    void Start()
    {
        slamVFX.Pause();
        health = maxHeight;

        //playerIndex = PlayerIndex.Two;
    }

    // Update is called once per frame
    void Update()
    {
        //if loss all health
        if (!alive)
        {
            //call death funcs for reindeer
            ReindeerDeath();
        }
        //else still alive
        else
        {
           // prevState = state;
            //state = GamePad.GetState(playerIndex);

            //check if santa exists
            CheckForSanta();

            //set rotation of reindeer based on stick input
            RotateReindeer();

            //attack inputs
            AttackInput();
        }
        
    }

    //logic for slam attack
    void SlamAttack()
    {
        if (player)
        {
            //if (slamVFX.isPaused) slamVFX.Play();
            //get direction vector of between reindeer and player
            Vector3 _vDirection = player.transform.position - transform.position;
            _vDirection.Normalize();
            _vDirection = _vDirection + new Vector3(0.0f, slamHeight, 0.0f);
            _vDirection *= slamStrength;
            //push player based on direction vector
            player.PushBack(_vDirection, slamStrength);
            //play audio
            slamAudio.Play();
        }
    }

    //logic for nose attack
    void NoseAttack()
    {
        //get point ahead of reindeer
        Vector3 FirePoint = transform.position + transform.forward + transform.up * 0.9f;
        //create projectile
        Instantiate(noseProjectile, FirePoint, transform.rotation);
    }

    //plays slam attack vfx
    public void EmitSlamVFX()
    {
        // Any parameters we assign in emitParams will override the current system's when we call Emit.
        // Here we will override the start color and size.
        var emitParams = new ParticleSystem.EmitParams();
        slamVFX.Emit(emitParams, 1);
        slamVFX.Play(); // Continue normal emissions
    }

    //called to decrease health of reindeer
    public void DecreaseHealth(float _Amount)
    {
        //if health is less than 0
        if (health <= 0.0f)
        {
            //set health to 0 <- clamps at 0
            health = 0.0f;
            //set scale of health bar
            healthBar.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
        }
        //else health not reached 0
        else if (health != 0.0f)
        {
            //remove set damage value from health
            health -= _Amount;
            //set scale of health bar
            healthBar.transform.localScale = new Vector3(health / maxHeight, 1.0f, 1.0f);
            //if health reaches 0, no longer alive
            if (health <= 0)
            {
                alive = false;
            }
        }
    }

    //sends game back to title scene
    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    //functionality when reindeer dies
    private void ReindeerDeath()
    {
        //do deathAudio stuff
        deathAudio.Play();
        GetComponent<Animator>().SetBool("Dead", true);
        player.GetComponent<Animator>().SetBool("Cheer", true);
    }

    //find santa
    private void CheckForSanta()
    {
        //if player ref is currently null and a game object with tag santa exists
        if (player == null && GameObject.FindGameObjectWithTag("Santa"))
        {
            //set player script ref to this santa object
            player = GameObject.FindGameObjectWithTag("Santa").GetComponent<PlayerController>();
        }
    }

    //uses joystick to rotate reindeer
    private void RotateReindeer()
    {
        //get stick direction as rotation
        Vector3 Direction = Vector3.forward /** state.ThumbSticks.Left.X*/ + Vector3.right /** -state.ThumbSticks.Left.Y*/;
        if (Direction.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(Direction, Vector3.up);
        }
    }

    //reindeer attack input logic
    private void AttackInput()
    {/*
        if (Time.time - timeSinceLastAttack > attackInputCooldown)
        {
            if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Pressed)
            {
                gameObject.GetComponent<Animator>().SetTrigger("Slam");
                timeSinceLastAttack = Time.time;
                jumpAudio.Play();
            }
            if (state.Triggers.Right > 0.5f)
            {
                gameObject.GetComponent<Animator>().SetTrigger("NoseAttack");
                timeSinceLastAttack = Time.time;
                noseLuanchAudio.Play();
            }
        }*/
    }
}
