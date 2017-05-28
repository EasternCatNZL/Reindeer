using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSplat : MonoBehaviour {

    public Image image;
	public float Duration = 2.0f;
	private float LastTime = 0.0f;
    //public GameObject image;
    
    //Color alpha;

    // Use this for initialization
    void Start ()
    {
        /*
        alpha = image.color;
        alpha.a = 1;
        image.enabled = false;
        image.color = alpha;
        */
        image.enabled = false;
		LastTime = Time.time;
        //alpha = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        //image.SetActive = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
    }

    public void FadeToTransparent()
    {
        image.CrossFadeAlpha(0, 6, true);             
    }
    
    public void ResetAlpha()
    {
        //image.color = alpha;
        //image.color = alpha;
		image.enabled = true;
        image.canvasRenderer.SetAlpha(1.0f);
		Invoke ("FadeToTransparent", Duration);
        //GetComponent<SpriteRenderer>().color.a(1.0f);
    }
    
    /*
    private void OnEnable()
    {

        image.color = alpha;
    }
    */
}
