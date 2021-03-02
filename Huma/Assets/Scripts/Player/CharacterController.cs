/*
 * 
 * EDITOR : KIM Ji hun 
 * Last Edit : 2021.2.19
 * Script Purpose : Player moving, rotating Controller
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public bool isAttacking = false;
    public bool isRolling = false;
    private Player PlayerFunc;

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

    #region Coroutine
    public Coroutine AttackCoroutine;
    Coroutine MoveByKeyCoroutine;
    Coroutine TurnAndStopCoroutine;
    #endregion

    private void Start()
    {
        mainCamera = Camera.main;
        PlayerFunc = GetComponent<Player>();
    }

    private void Move()
    {
        transform.Translate(new Vector3(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime));
        Vector3 dir = new Vector3(h, 0, v);
        if (MoveByKeyCoroutine == null)
            MoveByKeyCoroutine = StartCoroutine(MoveByKey(dir));
    }

    private IEnumerator MoveByKey(Vector3 dir)
    {
        float elapsedTime = 0;
        while (elapsedTime < rotSpeed)
        {
            Player.transform.forward = Vector3.Lerp(Player.transform.forward, dir, elapsedTime / rotSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        MoveByKeyCoroutine = null;

        yield return null;
    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if ((h != 1 || h != -1) || (v != 1 || v != -1) && !isAttacking)
        {
            Move();
        }

        Ray temp = new Ray(Vector3.zero, Vector3.zero);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            isAttacking = true;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            temp = ray;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10000f))
            {
                targetPos = hit.point;
            }

            if (TurnAndStopCoroutine == null)
                TurnAndStopCoroutine = StartCoroutine(TurnAndStop(targetPos));

            Debug.DrawRay(temp.origin, temp.direction * 100, Color.red);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            isRolling = true;
            StartCoroutine(MouseRight());
        }
    }

    public virtual IEnumerator MouseRight()
    {
        speed = 3 * speed;
        var time = 0;
        // while(time<)

        yield return null;
        yield break;
    }

    

    private IEnumerator TurnAndStop(Vector3 dir)
    {
        float elapsedTime = 0;
        Vector3 direction = dir - Player.transform.position;
        Vector3 dirXZ = new Vector3(direction.x, Player.transform.forward.y, direction.z);

        while (elapsedTime < rotSpeed)
        {
            Player.transform.forward = Vector3.Lerp(Player.transform.forward, dirXZ, elapsedTime / rotSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (AttackCoroutine == null) // Can't move while Attacking
            AttackCoroutine = StartCoroutine(PlayerFunc.Attack());

        TurnAndStopCoroutine = null;
        yield return null;

    }
}