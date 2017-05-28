using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBomb : MonoBehaviour {

	// Use this for initialization
	private SteamVR_TrackedObject trackedObj;
	public Transform HeadRef;
	public GameObject BombPrefab; //Reference to a rocket fist

    public float BombCooldown = 5;

    public Vector3 BombPositionOffset;

	public Vector3 Direction;
	public float Dot;

	public GameObject BombRef;
	public bool BombHandled = false;

    private float LastTime = 0.0f; //Last time since the bomb ability was used

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
        LastTime = Time.time;
		BombRef = Instantiate (BombPrefab, new Vector3(0.0f, -20.0f, 0.0f), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Controller.GetPressDown (Valve.VR.EVRButtonId.k_EButton_Grip) && Time.time - LastTime > BombCooldown)
        {
			GrabBomb ();
			LastTime = Time.time;
		}
        if (Controller.GetPressUp (Valve.VR.EVRButtonId.k_EButton_Grip) && BombHandled) 
		{
			BombRelease ();
		}
	}

	private FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	private  void GrabBomb()
	{
        Direction = trackedObj.transform.position - HeadRef.position;
		Dot = Vector3.Dot (Direction, HeadRef.forward);
		if (Dot < 0.0f && !BombHandled) 
		{         
            BombRef.transform.position = transform.position;
            BombRef.GetComponent<BombAbility>().Init();
			var joint = AddFixedJoint();
			joint.connectedBody = BombRef.GetComponent<Rigidbody>();
         

			BombHandled = true;
		}
	}

	private void BombRelease()
	{

		if (GetComponent<FixedJoint>())
		{

			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());

			BombRef.GetComponent<Rigidbody>().velocity = Controller.velocity;
			BombRef.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;

            print("Controller Values:" + Controller.velocity.ToString() + " / " + Controller.angularVelocity.ToString() );
            print("Bomb Values:" + BombRef.GetComponent<Rigidbody>().velocity.ToString() + " / " + BombRef.GetComponent<Rigidbody>().angularVelocity.ToString());

            BombHandled = false;
		}
			
	}
}
