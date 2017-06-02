using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour {

    private GameObject ButtonRef = null;

    // Use this for initialization
    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
        if(Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
        {
            print("Piano");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "VRButton")
        {
            ButtonRef = other.gameObject;
            ButtonRef.GetComponent<VRButton>().SetHighlight(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == ButtonRef)
        {
            ButtonRef.GetComponent<VRButton>().SetHighlight(false);
            ButtonRef = null;
        }
    }
}
