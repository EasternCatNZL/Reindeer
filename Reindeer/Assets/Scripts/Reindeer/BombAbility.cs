using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add to bomb prefab to make it explode!
public class BombAbility : MonoBehaviour {

	private bool NoPlayers = false;
    [Header("Bomb Settings")]
    public float ExplosionRadius = 2.0f;
	public float ExplosionDamage = 5.0f;
    public float KnockBackForce = 5.0f;
	public float KnockBackHeight = 5.0f;
    public GameObject BombExplosionVFX = null;

    private GameObject[] Players = new GameObject[3];

    [Header("Audio")]
    public AudioSource bombExplosionSound;

	// Use this for initialization
	void Awake () {
        Players = GameObject.FindGameObjectsWithTag("Player");
		if (Players.Length == 0) {
			NoPlayers = true;
		}
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	}

    public void Init()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(BombExplosionVFX, transform.position, BombExplosionVFX.transform.rotation);
        DamagePlayers();
        //vary pitch
        bombExplosionSound.pitch = Random.Range(0.6f, 1.0f);
        //play sound
        bombExplosionSound.Play();
        ResetBomb();
    }

    void DamagePlayers()
    {
        Vector3 _Distance = Vector3.zero;
        //Loops through the players and checks distance against explosion radius
		if (!NoPlayers) {
			foreach (GameObject _Player in Players) {
				_Distance = _Player.transform.position - transform.position;
				if (_Distance.magnitude < ExplosionRadius) {
					_Player.GetComponent<Rigidbody> ().AddForce (((_Player.transform.position - transform.position) * KnockBackForce) + new Vector3 (0.0f, KnockBackHeight, 0.0f), ForceMode.Impulse);
					_Player.GetComponent<HealthManagement> ().DecreaseHealth (ExplosionDamage);
				}
			}
		}
    }

    //Hides the bomb under the arena for further use
    private void ResetBomb()
    {
        transform.position = new Vector3(0.0f, -20.0f, 0.0f);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
           
    }
}
