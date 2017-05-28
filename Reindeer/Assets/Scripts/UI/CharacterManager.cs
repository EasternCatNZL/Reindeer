using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class CharacterManager : MonoBehaviour {
    public int GameSceneIndex = -1;

     public GameObject PlayerOneSelector;
     public GameObject PlayerTwoSelector;
     public GameObject PlayerThreeSelector;

    private bool Loaded = false;


	 private int ConfirmedPlayers = 0;
     private int SantaPlayer = 0; //Index of the player playing Santa
     private int ExPlayer = 0; //Index of the player playing the Ex
     private int SledPlayer = 0; //Index of the player playing the Sled

	// Use this for initialization
	void Start () {
        if (GameSceneIndex <= 0) Debug.LogError("Game Scene Index is not set in the Character Manager");
        PlayerOneSelector = GameObject.Find("PlayerOneCharacter");
        PlayerTwoSelector = GameObject.Find("PlayerTwoCharacter");
        PlayerThreeSelector = GameObject.Find("PlayerThreeCharacter");
		Object.DontDestroyOnLoad (this);
    }
	
	// Update is called once per frame
	void Update () {
		if (ConfirmedPlayers >= 3 && !Loaded) {
			SceneManager.LoadScene (GameSceneIndex);
            Loaded = true;
		}
	}

	public PlayerIndex[] GetCharacters()
	{
		PlayerIndex[] Characters = new PlayerIndex[3];
		switch (SantaPlayer) 
		{
		case 1:
			Characters [0] = PlayerIndex.One;
			break;
		case 2:
			Characters [0] = PlayerIndex.Two;
			break;
		case 3:
			Characters [0] = PlayerIndex.Three;
			break;
		default:
			Characters [0] = PlayerIndex.Four;
			break;
		}

		switch (ExPlayer) 
		{
		case 1:
			Characters [1] = PlayerIndex.One;
			break;
		case 2:
			Characters [1] = PlayerIndex.Two;
			break;
		case 3:
			Characters [1] = PlayerIndex.Three;
			break;
		default:
			Characters [1] = PlayerIndex.Four;
			break;
		}

		switch (SledPlayer) 
		{
		case 1:
			Characters [2] = PlayerIndex.One;
			break;
		case 2:
			Characters [2] = PlayerIndex.Two;
			break;
		case 3:
			Characters [2] = PlayerIndex.Three;
			break;
		default:
			Characters [2] = PlayerIndex.Four;
			break;
		}
		return Characters;
	}

     public bool ConfirmCharacter(int _PlayerIndex , int _Character )
    {
        switch (_Character)
        {
            case 0:
                if (SantaPlayer == 0)
                {
                    SantaPlayer = _PlayerIndex;
                    GreyCharacter(_Character);
				ConfirmedPlayers++;
                    return true;
                }
                else
                {
                    return false;
                }
            case 1:
                if(ExPlayer == 0)
                {
                    ExPlayer = _PlayerIndex;
                    GreyCharacter(_Character);
				ConfirmedPlayers++;
                    return true;
                }
                else
                {
                    return false;
                }
            case 2:
                if (SledPlayer == 0)
                {
                    SledPlayer = _PlayerIndex;
                    GreyCharacter(_Character);
				ConfirmedPlayers++;
                    return true;
                }
                else
                {
                    return false;
                }
        }
        return false;
    }

     public void UnconfirmCharacter(int _PlayerIndex, int _Character)
    {
        switch (_Character)
        {
            case 0:
                SantaPlayer = 0;
			ConfirmedPlayers--;
                break;
            case 1:
                ExPlayer = 0;
			ConfirmedPlayers--;
                break;
            case 2:
                SledPlayer = 0;
			ConfirmedPlayers--;
                break;
        }
        ColourCharacter(_Character);
    }

     void GreyCharacter(int _Character)
    {
        PlayerOneSelector.GetComponent<CharacterSelector>().GreyCharacter(_Character);
        PlayerTwoSelector.GetComponent<CharacterSelector>().GreyCharacter(_Character);
        PlayerThreeSelector.GetComponent<CharacterSelector>().GreyCharacter(_Character);
    }

     void ColourCharacter(int _Character)
    {
        PlayerOneSelector.GetComponent<CharacterSelector>().ColourCharacter(_Character);
        PlayerTwoSelector.GetComponent<CharacterSelector>().ColourCharacter(_Character);
        PlayerThreeSelector.GetComponent<CharacterSelector>().ColourCharacter(_Character);
    }

}
