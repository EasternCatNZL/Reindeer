using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    
	private Transform PlayerRef = null;
    private GameObject TrackingTarget = null;
    private float CameraHeight = 0.0f;
    private float CameraDistance = 0.0f;
    // Use this for initialization
    void Start()
    {

    }

    public void Init(Transform _Player, float _NewCameraHeight, float _NewCameraDistance)
    {
		PlayerRef = _Player;
        CameraHeight = _NewCameraHeight;
        CameraDistance = _NewCameraDistance;

		Vector3 temp = _Player.position - TrackingTarget.transform.position;
		Vector3.Normalize (temp);
		temp = temp * CameraDistance;

        //transform.SetParent(_Player);
		transform.localPosition = _Player.transform.position + temp;

    }

    // Update is called once per frame
    void Update()
    {
		Vector3 temp = PlayerRef.position - TrackingTarget.transform.position;
		temp.Normalize ();

        temp = temp * CameraDistance;

		transform.localPosition = PlayerRef.transform.position + temp + new Vector3(0.0f, CameraHeight, 0.0f);

        transform.LookAt(TrackingTarget.transform);

		transform.localEulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0.0f); //Smooth Camera
    }

    public void SetTarget(GameObject _NewTarget)
    {
        TrackingTarget = _NewTarget;

    }
}
