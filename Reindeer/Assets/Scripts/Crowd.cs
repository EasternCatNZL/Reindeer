using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{

    public int CheerType = 0;
    private float m_fAnimSpeed = 1;

    // Use this for initialization
    void Start()
    {

        if (CheerType == 0) CheerType = (int)Mathf.Ceil(Random.Range(0.1f, 4));
        m_fAnimSpeed = Random.Range(1.0f, 1.5f);
        gameObject.GetComponent<Animator>().SetInteger("Type", CheerType);
        gameObject.GetComponent<Animator>().speed = m_fAnimSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
