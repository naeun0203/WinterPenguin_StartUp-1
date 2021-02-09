using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region DirectionVar
    private float h;
    private float v;
    public float speed;
    public GameObject Player;
    #endregion DirectionVar

    #region Ray
    private Camera mainCamera;
    private Vector3 targetPos;
    public float rotSpeed;
    #endregion Ray

    private void Start()
    {
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
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            temp = ray;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000f))
            {
                targetPos = hit.point;
            }
            StartCoroutine(TurnAndStop());
        }

        Debug.DrawRay(temp.origin, temp.direction * 100, Color.red);
    }

    private void Turn(Vector3 targetPos,float elapsedTime)
    {
        Vector3 dir = targetPos - Player.transform.position;
        Vector3 dirXZ = new Vector3(dir.x, Player.transform.forward.y, dir.z);

        Player.transform.forward = Vector3.Lerp(Player.transform.forward, dirXZ, elapsedTime / rotSpeed) ;
    }

    private IEnumerator TurnAndStop()
    {
        float elapsedTime = 0;
        while (elapsedTime < rotSpeed)
        {
            Turn(targetPos, elapsedTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }
}
