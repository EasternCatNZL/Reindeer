using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAbility : MonoBehaviour {
    public bool DebugSpawn = false;
    [Header("Missile Settings")]
    public float MissileCooldown = 10.0f;
    public int MissileNumber = 20;
    public float OuterRadius = 5.0f;
    public float InnerRadius = 2.0f;
    [Header("Prefab")]
    public GameObject MissilePrefab = null;
    public GameObject TargetPrefab = null;

    public List<GameObject> Targets; //Doesn't work if set to private :'(
    public List<GameObject> Missiles;

    private float LastTime = 0.0f;

    // Use this for initialization
    void Start()
    {
        LastTime = Time.time;

        for (int i = 0; i < MissileNumber; ++i)
        {
            Targets.Add(Instantiate(TargetPrefab, new Vector3(0.0f, -20.0f, 0.0f), Quaternion.identity));
        }
        for (int i = 0; i < MissileNumber; ++i)
        {
            Missiles.Add(Instantiate(MissilePrefab, new Vector3(0.0f, -20.0f, 0.0f), Random.rotation));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(DebugSpawn)
        {
            SpawnMissiles();
            DebugSpawn = false;
        }
    }

    //Spawn Missiles
    public void SpawnMissiles()
    {
        float x = 0.0f;
        float z = 0.0f;

        if (Time.time - LastTime > MissileCooldown)
        {
            for (int i = 0; i < MissileNumber; ++i)
            {
                x = Random.Range(-OuterRadius, OuterRadius);
                z = Random.Range(-OuterRadius, OuterRadius);
                while (x < InnerRadius && x > -InnerRadius && z < InnerRadius && z > -InnerRadius)
                {
                    x = Random.Range(-OuterRadius, OuterRadius);
                    z = Random.Range(-OuterRadius, OuterRadius);
                }
                Vector3 temp = new Vector3(x, 0.4f, z);
                Targets[i].transform.position = temp;
            }
            for (int j = 0; j < MissileNumber; ++j)
            {
                Missiles[j].GetComponent<ReindeerMissileTest>().SetTargetPosition(Targets[j].transform.position);
                Missiles[j].GetComponent<ReindeerMissileTest>().SetPosition(transform.position + new Vector3(0.0f, 2.0f, 0.0f));
                Missiles[j].GetComponent<ReindeerMissileTest>().Launch();
            }

            LastTime = Time.time;
        }
    }

    public void ResetTargets()
    {
        foreach (GameObject _Target in Targets)
        {
            _Target.transform.position = new Vector3(0.0f, -20.0f, 0.0f);
        }
    }
}
