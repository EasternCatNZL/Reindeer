using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManagement : MonoBehaviour
{

    public bool DebugDamage = false;

    public float MaximumHealth = 100.0f; //Max Health
    public float Health = 0.0f; //Current Health

    public GameObject Healthbar = null;

    public GameObject DeathVFX = null;

    public bool Shielded = false;

    //audio
    public AudioSource takeDamageSound;
    public AudioSource deathSound;
    public AudioSource reindeerHalfHealthSound;

    // Use this for initialization
    void Start()
    {
        Health = MaximumHealth;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (DebugDamage)
        {
            DecreaseHealth(5);
            DebugDamage = false;
        }
        if (Health <= 0.0f)
        {
            if (gameObject.tag != "Reindeer")
            {
                GetComponent<BasicMovement>().SetDisabled(true);
				Healthbar.SetActive (false);
            }
            if (DeathVFX)
            {
                Instantiate(DeathVFX, transform.position, DeathVFX.transform.rotation);
            }
            GetComponent<Animator>().SetTrigger("Death");
            if(deathSound)deathSound.Play();
            //Insert end game stuff here
        }
        if(gameObject.tag == "Reindeer")
        {
            if(Health < MaximumHealth/2)
            {
                GetComponent<MiniHealthReindeerTest>().SpawnActive = true;
                //if reindeer
                if (reindeerHalfHealthSound)
                {
                    reindeerHalfHealthSound.Play();
                }
            }
        }
    }

    void UpdateUI()
    {
        Vector3 newScale = new Vector3(Health / MaximumHealth, Healthbar.transform.localScale.y, Healthbar.transform.localScale.z);
        Healthbar.transform.localScale = newScale;
    }

    public float GetHealth()
    {
        return Health;
    }

    public void DecreaseHealth(float _Damage)
    {
        if (Health > 0.0 && !Shielded)
        {
            Health -= _Damage;
            if (takeDamageSound)
            {
                takeDamageSound.pitch = Random.Range(0.6f, 1.0f);
                takeDamageSound.Play();
            }
            if (Health <= 0.0f)
            {
                Health = 0.0f;
                if (gameObject.tag == "Player")
                {
                    GameManagement.GetGameManager().GetComponent<GameManagement>().SetDead(false);
					GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
                    
                }
                else
                {
                    GameManagement.GetGameManager().GetComponent<GameManagement>().SetDead(true);
                    if(deathSound)deathSound.Play();
                }
            }
            UpdateUI();
        }
    }

    public void IncreaseHealth(float _Heal)
    {
        if (Health != MaximumHealth)
        {
            Health += _Heal;
            UpdateUI();
        }
    }
}
