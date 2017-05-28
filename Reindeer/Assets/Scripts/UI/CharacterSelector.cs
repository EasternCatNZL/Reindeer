using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class CharacterSelector : MonoBehaviour
{

    public GameObject CharManager;
    public PlayerIndex Player;

    public Image Join = null;
    public Image LeftArrow = null;
    public Image RightArrow = null;
    public GameObject ReadySign = null;
    public Sprite[] CharacterSprites = new Sprite[3];
    public Sprite[] ColourSprites = new Sprite[3];
    public Sprite[] GreySprites = new Sprite[3];
    public Sprite[] ArrowSprites = new Sprite[4];

    [Header("Audio")]
    public AudioSource changeSelectSound;
    public AudioSource santaSound;
    public AudioSource sledSound;
    public AudioSource exSound;


    private bool PlayerJoined = false;
    private bool Selected = false;
    private int CharacterSelected = 0;

    private GamePadState prevState;
    private GamePadState State;

    // Use this for initialization
    void Start()
    {
        GetComponent<Image>().sprite = CharacterSprites[CharacterSelected % 3];
        GreyArrows();
    }

    // Update is called once per frame
    void Update()
    {

        prevState = State;
        State = GamePad.GetState(Player);



        if (State.IsConnected)
        {
            if (!PlayerJoined)
            {
                if (prevState.Buttons.A == ButtonState.Released && State.Buttons.A == ButtonState.Pressed)
                {
                    PlayerJoined = true;
                    GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
                    Join.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
                    ColourArrows();
                }
            }
            else if (!Selected)
            {
                //Check button presses
                //Change Chraracter
                if (prevState.DPad.Right == ButtonState.Released && State.DPad.Right == ButtonState.Pressed)
                {
                    CharacterSelected++;
                    //vary pitch
                    changeSelectSound.pitch = Random.Range(0.6f, 1.0f);
                    //play sound
                    changeSelectSound.Play();
                }
                else if (prevState.DPad.Left == ButtonState.Released && State.DPad.Left == ButtonState.Pressed)
                {
                    CharacterSelected--;
                    //vary pitch
                    changeSelectSound.pitch = Random.Range(0.6f, 1.0f);
                    //play sound
                    changeSelectSound.Play();
                }
                if (CharacterSelected < 0)
                {
                    CharacterSelected = 2;
                }
                GetComponent<Image>().sprite = CharacterSprites[CharacterSelected % 3];
                //Select Character
                if (prevState.Buttons.A == ButtonState.Released && State.Buttons.A == ButtonState.Pressed)
                {

                    if (CharManager.GetComponent<CharacterManager>().ConfirmCharacter(PlayerIndexToInt(Player), CharacterSelected % 3))
                    {
                        Selected = true;
                        ReadySign.SetActive(true);
                        GreyArrows();
                        if (CharacterSelected == 0)
                        {
                            santaSound.Play();
                        }
                        else if(CharacterSelected == 1)
                        {
                            sledSound.Play();
                        }
                        else
                        {
                            exSound.Play();
                        }
                    }
                }
            }
            //Deselect Character
            else if (Selected && prevState.Buttons.B == ButtonState.Released && State.Buttons.B == ButtonState.Pressed)
            {
                CharManager.GetComponent<CharacterManager>().UnconfirmCharacter(PlayerIndexToInt(Player), CharacterSelected % 3);
                Selected = false;
                ReadySign.SetActive(false);
                ColourArrows();
            }
        }
    }

    public void GreyCharacter(int _Character)
    {
        print("Greying Character: " + _Character);
        CharacterSprites[_Character] = GreySprites[_Character];
        GetComponent<Image>().sprite = CharacterSprites[CharacterSelected % 3];
    }

    public void ColourCharacter(int _Character)
    {
        CharacterSprites[_Character] = ColourSprites[_Character];
        GetComponent<Image>().sprite = CharacterSprites[CharacterSelected % 3];
    }

    void GreyArrows()
    {
        LeftArrow.sprite = ArrowSprites[2];
        RightArrow.sprite = ArrowSprites[3];
    }

    void ColourArrows()
    {
        LeftArrow.sprite = ArrowSprites[0];
        RightArrow.sprite = ArrowSprites[1];
    }

    int PlayerIndexToInt(PlayerIndex _PlayerIndex)
    {
        if (_PlayerIndex == PlayerIndex.One)
        {
            return 1;
        }
        else if (_PlayerIndex == PlayerIndex.Two)
        {
            return 2;
        }
        else if (_PlayerIndex == PlayerIndex.Three)
        {
            return 3;
        }
        else if (_PlayerIndex == PlayerIndex.Four)
        {
            return 4;
        }
        return 0;
    }
}
