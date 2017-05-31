using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICooldownIcons : MonoBehaviour {

	public Sprite ReadyIcon = null;
	public Sprite GreyIcon = null;

	// Use this for initialization
	void Start () {
		SetReady ();
	}
	
	public void SetReady()
	{
		GetComponent<SpriteRenderer> ().sprite = ReadyIcon;
	}

	public void SetGrey()
	{
		GetComponent<SpriteRenderer> ().sprite = GreyIcon;
	}
}
