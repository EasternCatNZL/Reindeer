using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMissiles : MonoBehaviour
{
    public bool Spawn = false;
    public GameObject ReindeerRef; //Reference to a rocket fist

    private Vector3 LastPosition;
    private Vector3 CurrentPosition;

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

    // Update is called once per frame
    void Update()
    {

        if (Controller.GetHairTriggerDown())
        {
            LastPosition = trackedObj.transform.position;
        }


        if (Controller.GetHairTriggerUp())
        {
            CurrentPosition = trackedObj.transform.position;
            if (CurrentPosition.y - LastPosition.y < -0.5)
            {
                print("FIRE THE MISSILES!!!");
                print((CurrentPosition - LastPosition).ToString());
                ReindeerRef.GetComponent<MissileAbility>().SpawnMissiles();
            }
        }
    }
}
