﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBall : MonoBehaviour
{
    public GameObject prefab;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SpawnBall();
        }
    }

    public void SpawnBall()
    {
        Instantiate(prefab, new Vector3(-1.6F, 2, 0), Quaternion.identity);
    }
}
