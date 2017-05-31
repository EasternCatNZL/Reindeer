using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class SlideShow : MonoBehaviour {

    GamePadState State;
    GamePadState prevState; 

    public Sprite[] ControlImages = new Sprite[3];
    private int CurrentImage = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        prevState = State;
        State = GamePad.GetState(PlayerIndex.One);
        if (prevState.Buttons.A == ButtonState.Released && State.Buttons.A == ButtonState.Pressed)
        {
            NextSlide();
        }

    }

    void NextSlide()
    {
        if (CurrentImage < 3)
        {
            CurrentImage++;
            GetComponent<Image>().sprite = ControlImages[CurrentImage];
        }
        else if(CurrentImage == 3)
        {
            gameObject.SetActive(false);
        }
    }
}
