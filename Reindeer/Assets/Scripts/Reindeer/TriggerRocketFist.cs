using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRocketFist : MonoBehaviour
{


    // Use this for initialization
    private SteamVR_TrackedObject trackedObj;
    public GameObject RocketFist; //Reference to a rocket fist

    //Laser
    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;
    public LayerMask aimMask;

    public Animator HandAnimator;

    public bool LaserActive = false;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.GetHairTriggerDown() || LaserActive)
        {
            RaycastHit hit;

            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, aimMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                HandAnimator.SetBool("Closed", true);
                LaserActive = true;
            }
            else
            {
                laser.SetActive(false);
                HandAnimator.SetBool("Closed", false);
                LaserActive = false;
            }
        }
        if (Controller.GetHairTriggerUp() && LaserActive)
        {
            laser.SetActive(false);
            HandAnimator.SetBool("Closed", false);
            LaserActive = false;
            RocketFist.GetComponent<RocketFist>().Launch();
        }
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }
}
