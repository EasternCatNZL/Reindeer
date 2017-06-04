using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using XInputDotNetPure;
using UnityEngine.SceneManagement;

public class Reindeer : MonoBehaviour
{
    //reindeer health var
    public float maxHeight = 1000; //health of the reindeer
    public Image healthBar; //reference to health bar image
    public AudioSource deathAudio; //audio reference to reindeer death sound effect

    private float health = 0; //current health of the reindeer
    private bool alive = true; //check for whether reindeer is alive

    //controller plugin vars
    private bool playerIndexSet = false;
    //private PlayerIndex playerIndex;
    //private GamePadState state;
    //private GamePadState prevState;

    // Use this for initialization
    void Start()
    {
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
            //set rotation of reindeer based on stick input
            RotateReindeer();
        }
        
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
}
