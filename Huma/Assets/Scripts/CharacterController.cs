using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region DirectionVar
    private float h;
    private float v;
    Rigidbody rb;
    public float speed;
    private float velocity;
    public GameObject Player;
    #endregion DirectionVar

    #region Ray
    private Camera mainCamera;
    private Vector3 targetPos;
    public float rotSpeed;
    #endregion Ray

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }


    private void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime));
    }

    private void Update()
    {
        Ray temp = new Ray(Vector3.zero, Vector3.zero);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            temp = ray;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000f))
            {
                targetPos = hit.point;
            }
            Turn(targetPos);
        }
        
        Debug.DrawRay(temp.origin, temp.direction * 100, Color.red);
    }

    private void Turn(Vector3 targetPos)
    {
        Vector3 dir = targetPos - Player.transform.position;
        Vector3 dirXZ = new Vector3(dir.x, Player.transform.position.y, dir.z);

    }
}
