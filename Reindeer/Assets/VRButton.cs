using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRButton : MonoBehaviour {

    private float HighlightSize = 0.1f;
    private Vector3 OriginalScale;

	// Use this for initialization
	void Start () {
        OriginalScale = transform.localScale;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetHighlight(bool _State)
    {
        if(_State)
        {
            transform.localScale = OriginalScale + OriginalScale * 0.1f;
        }
        else
        {
            transform.localScale = OriginalScale;
        }
    }

    public void SetHighlightSize(float _NewSize)
    {
        HighlightSize = _NewSize;
    }

}
