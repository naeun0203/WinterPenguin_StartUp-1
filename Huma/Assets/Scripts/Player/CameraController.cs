using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera mainCamera;
    GameObject player;

    private void Awake()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        mainCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 9, player.transform.position.z - (player.transform.position.y+9)*(1/Mathf.Sqrt(3)));
        mainCamera.transform.rotation = Quaternion.LookRotation(player.transform.position - mainCamera.transform.position );
    }
}
