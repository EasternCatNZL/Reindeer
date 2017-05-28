using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class GameManagement : MonoBehaviour {

    private static GameObject GameManager;
	private GameObject CharacterManager = null;
	public GameObject Santa = null;
	public GameObject Ex = null;
	public GameObject Sled = null;
    public GameObject Reigndeer = null;

    public int PlayersDead = 0;
    private bool ReigndeerDead = false;

	private PlayerIndex[] PlayerIndexes = new PlayerIndex[3];

    [Header("Victory Images")]
    public GameObject ReigndeerWon;
    public GameObject ReigndeerLoss;
    public GameObject PlayersWon;
    public GameObject PlayersLoss;

	// Use this for initialization
	void Start () {
        GameManager = gameObject;
		CharacterManager = GameObject.Find ("CharManager");
		PlayerIndexes = CharacterManager.GetComponent<CharacterManager> ().GetCharacters ();

		if (PlayerIndexes [0] != PlayerIndex.Four) {
			Santa.GetComponent<SantaAttack> ().index = PlayerIndexes [0];
			Santa.GetComponent<BasicMovement> ().PlayerNumber = PlayerIndexes [0];
		}
		if (PlayerIndexes [1] != PlayerIndex.Four) {
			Ex.GetComponent<ShieldAttack> ().index = PlayerIndexes [1];
			Ex.GetComponent<BasicMovement> ().PlayerNumber = PlayerIndexes [1];
		}
		if (PlayerIndexes [2] != PlayerIndex.Four) {
			Sled.GetComponent<SledAttack> ().index = PlayerIndexes [2];
			Sled.GetComponent<BasicMovement> ().PlayerNumber = PlayerIndexes [2];
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDead(bool _Reigndeer)
    {
        if(!_Reigndeer)
        {
            PlayersDead++;
            if(PlayersDead >= 3)
            {
                Endgame("Reigndeer");
            }
        }
        else
        {
            ReigndeerDead = true;
            Endgame("Players");
        }

    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void Endgame(string _WhoWon)
    {
        if(_WhoWon.Contains("Players"))
        {
            ReigndeerLoss.SetActive(true);
            PlayersWon.SetActive(true);
            Invoke("LoadMainMenu", 4);
        }
        else if(_WhoWon.Contains("Reigndeer"))
        {
            ReigndeerWon.SetActive(true);
            PlayersLoss.SetActive(true);
            Invoke("LoadMainMenu", 4);
        }

    }

    public static GameObject GetGameManager()
    {
        return GameManager;
    }
}
