﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera mainCamera;
    GameObject player;

    private void Awake()
    {
        mainCamera = Camera.main;
        player = GameObject.Find("Player");
    }

    private void LateUpdate()
    {
        mainCamera.transform.position = new Vector3(player.transform.localPosition.x, player.transform.position.y + 9, player.transform.localPosition.z);
    }
}
