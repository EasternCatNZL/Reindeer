using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;


public class TitleScreen : MonoBehaviour
{


    private GamePadState state;
    private GamePadState prevState;
    /*
     * 0 = postive
     * 1 = negative
     */
    [Header("Images")]
    public Sprite[] startImage = new Sprite[2]; //start images
    public Sprite[] creditImage = new Sprite[2]; //credit images
    public Sprite[] quitImage = new Sprite[2]; //quit images
    [Header("Button References")]
    public Image startRef; //reference to start
    public Image creditRef; //reference to credits
    public Image quitRef; //reference to quit
    public Text Continue;
    [Header("Image References")]
    public Image creditsPage; //reference to credits page
    public Image Controls;
    public Image Instructions;
    [Header("Controller Settings")]
    public float Deadzone;
    public float MenuDelayTime = 0.5f; //Prevents the thumbstick from navigating the menu really fastly


    private bool Begin = false;
    private bool Loading = false;
    private AsyncOperation loadProg;
    private float timer = 3.0f;
    private float lastTime = 0.0f;
    /*
     *button choice
     * 0 = start
     * 1 = credit
     * 2 = end
     */

    int currentButton = 0; //int reference to currently selected button
    public bool canInput = false; //checks to see if input from controller axis allowed
    bool showingCredits = false; //checks to see if credits is currently being shown

    // Use this for initialization
    void Start()
    {
        canInput = true;
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(PlayerIndex.One);
        if (state.IsConnected)
        {
            canInput = true;
        }
        if (showingCredits)
        {
            ConfirmCredits();
        }
        else
        {
            ChangeChoice();
            GetSelection();
        }
        //Load next scene while showing controls and instructions
        //if (Loading)
        //{
        //    print("Begin:");
        //    print(Begin);
        //    if (Begin /*&&  state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Pressed*/)
        //    {
        //        loadProg.allowSceneActivation = true;
        //    }
        //    //if (Input.anyKeyDown && Time.time - lastTime <= timer)
        //    //{
        //    //    print(loadProg.isDone);
        //    //    print(loadProg.progress);
        //    //    Controls.color = new Color(1, 1, 1, 0);
        //    //    Instructions.color = new Color(1, 1, 1, 1);
        //    //    if (loadProg.progress >= 0.9f)
        //    //    {
        //    //        Continue.color = new Color(1, 1, 1, 1);
        //    //        Begin = true;
        //    //    }
        //    //}
        //    //print("Loading Progress");
        //    //print(loadProg.progress);
        //    if (Time.time - lastTime >= timer)
        //    {
        //        print(loadProg.isDone);

        //        Controls.color = new Color(1, 1, 1, 0);
        //        Instructions.color = new Color(1, 1, 1, 1);
        //        if (loadProg.progress >= 0.9f)
        //        {
        //            Continue.color = new Color(1, 1, 1, 1);
        //            Begin = true;
        //        }
        //    }
        //}
    }


    //change the choice on start menu
    void ChangeChoice()
    {
        //check if player can input
        if (canInput && Time.time - lastTime > MenuDelayTime)
        {
            //check for controller positive inputs
            if (state.ThumbSticks.Left.Y > Deadzone || state.DPad.Up == ButtonState.Pressed && prevState.DPad.Up == ButtonState.Released)
            {
                //check current button
                if (currentButton == 0)
                {
                    //increment up
                    currentButton = 1;
                    //change graphics
                    startRef.sprite = startImage[1];
                    creditRef.sprite = creditImage[0];
                    quitRef.sprite = quitImage[1];
                }
                else if (currentButton == 1)
                {
                    //increment up
                    currentButton = 2;
                    //change graphics
                    startRef.sprite = startImage[1];
                    creditRef.sprite = creditImage[1];
                    quitRef.sprite = quitImage[0];
                }
                else if (currentButton == 2)
                {
                    //increment up
                    currentButton = 0;
                    //change graphics
                    startRef.sprite = startImage[0];
                    creditRef.sprite = creditImage[1];
                    quitRef.sprite = quitImage[1];
                }
                lastTime = Time.time;
            }
            //else check for negative controller inputs
            else if (state.ThumbSticks.Left.Y < -Deadzone || state.DPad.Down == ButtonState.Pressed && prevState.DPad.Down == ButtonState.Released)
            {
                //check current button
                if (currentButton == 0)
                {
                    //decrement up
                    currentButton = 2;
                    //change graphics
                    startRef.sprite = startImage[1];
                    creditRef.sprite = creditImage[1];
                    quitRef.sprite = quitImage[0];
                }
                else if (currentButton == 1)
                {
                    //decrement up
                    currentButton = 0;
                    //change graphics
                    startRef.sprite = startImage[0];
                    creditRef.sprite = creditImage[1];
                    quitRef.sprite = quitImage[1];
                }
                else if (currentButton == 2)
                {
                    //decrement up
                    currentButton = 1;
                    //change graphics
                    startRef.sprite = startImage[1];
                    creditRef.sprite = creditImage[0];
                    quitRef.sprite = quitImage[1];
                }
                lastTime = Time.time;
            }

        }
    }

    void GetSelection()
    {
        //check input for controller submit
        if (state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released)
        {
            //check which button is currently selected
            if (currentButton == 0)
            {
                //load into game scene
                SceneManager.LoadScene(1);
                //Controls.color = new Color(1, 1, 1, 1);
                //loadProg = SceneManager.LoadSceneAsync(1);
                //loadProg.allowSceneActivation = false;
                //lastTime = Time.time;
                Loading = true;
            }
            else if (currentButton == 1)
            {
                //show credits
                creditsPage.color = new Color(1, 1, 1, 1);
                showingCredits = true;
            }
            else if (currentButton == 2)
            {
                //quit application
                Application.Quit();
            }
        }
    }

    void ConfirmCredits()
    {
        if (state.Buttons.B == ButtonState.Pressed && prevState.Buttons.B == ButtonState.Released)
        {
            creditsPage.color = new Color(1, 1, 1, 0);
            showingCredits = false;
        }
    }
}