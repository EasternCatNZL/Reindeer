using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

    // Use this for initialization
    private SteamVR_TrackedObject trackedObj;

 
    public GameObject collidingObject;
  
    public GameObject objectInHand;

    public Animator HandAnimator;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = col.gameObject;
    }

    // Update is called once per frame
    void Update () {
        if (Controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
        {
            if (collidingObject)
            {
                HandAnimator.SetBool("Closed", true);
                GrabObject();
            }
        }


        if (Controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_Grip))
        {
            if (objectInHand)
            {
                HandAnimator.SetBool("Closed", false);
                ReleaseObject();
            }
            else if(GetComponent<FixedJoint>())
            {
                Destroy(GetComponent<FixedJoint>());
            }
        }
    }

    private void GrabObject()
    {
  
        objectInHand = collidingObject;
        collidingObject = null;
       
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private void ReleaseObject()
    {
       
        if (GetComponent<FixedJoint>())
        {
          
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
          
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }

        objectInHand = null;
    }


    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

  
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "ReindeerHand")
        {
            SetCollidingObject(other);
        }
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "ReindeerHand")
        {
            SetCollidingObject(other);
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "ReindeerHand")
        {
            if (!collidingObject)
            {
                return;
            }

            collidingObject = null;
        }
    }
}
