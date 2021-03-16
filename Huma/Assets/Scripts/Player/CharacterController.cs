/*
 * 
 * EDITOR : KIM Ji hun 
 * Last Edit : 2021.2.19
 * Script Purpose : Player moving, rotating Controller
 * 
 */

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

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
    Coroutine MouseRightCoroutine;

    #endregion

    private void Start()
    {
        mainCamera = Camera.main;
        PlayerFunc = GetComponent<Player>();
    }

    private void Move()
    {
        Vector3 dir = new Vector3(h, 0, v);
        if (dir.magnitude > 1)
        {
            dir = dir.normalized;
        }
        transform.Translate(dir * (speed * Time.deltaTime));
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

        yield break;
    }

    private void Update()
    {
        if (!isRolling) // if player is not rolling
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

            if ((h != 0 || v != 0) && !isAttacking) Move(); // Move

            if (Input.GetKey(KeyCode.Mouse0)) // Attack
            {
                isAttacking = true;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                var temp = ray;
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 10000f))
                {
                    targetPos = hit.point;
                }

                if (TurnAndStopCoroutine == null)
                    TurnAndStopCoroutine = StartCoroutine(TurnAndStop(targetPos));
                
                
#if UNITY_EDITOR
                Debug.DrawRay(temp.origin, temp.direction * 100, Color.red);
#endif
            } 
            
            if (Input.GetKeyDown(KeyCode.Mouse1)) // Rolling forward
            {
                if (MouseRightCoroutine == null)
                {
                    isRolling = true;
                    MouseRightCoroutine = StartCoroutine(MouseRight());
                }
            }
        }
        

        
    }

    #region RollingParam
    float rollingTime = .5f;
    float rollingSpeed = 2f;
    #endregion

    public virtual IEnumerator MouseRight()
    {
        speed =  speed*rollingSpeed; // increase speed as Multiply
        float time = 0;
        Vector3 des = transform.position + (Player.transform.forward.normalized * (speed * rollingTime)); // Calc destination with speed and time for roll forward
        while (time<rollingTime)
        {
            transform.position = Vector3.Lerp(transform.position, des, time/rollingTime); // Lerp Character each frames
            time += Time.deltaTime;
            yield return null;
        }

        speed = speed / rollingSpeed;
        isRolling = false;
        MouseRightCoroutine = null;
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
        yield break;
    }
}