//************************************************
// EDITOR : JNE
// LAST EDITED DATE : 2020.02.22
// Script Purpose : Monster_Spiter Attack, Spit parabola
//******************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiterAttack : MonoBehaviour
{
    public float SpitListSize = 3;

    private Transform playerTransform;

    private float firingAngle = 45.0f;
    private float gravity = 9.8f;

    List<GameObject> SpitList;
    public GameObject Spit;
    public GameObject SpiterMouth;

    MonsterBase AtkSpit;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        AtkSpit = this.gameObject.GetComponent<MonsterBase>();
        SpitList = new List<GameObject>();
        for (int i = 0; i < SpitListSize; i++)
        {
            GameObject objSpit = (GameObject)Instantiate(Spit);
            objSpit.SetActive(false);
            SpitList.Add(objSpit);
        }
    }

    void Update()
    {
        if (AtkSpit.isAttack == true)
        {
            Fire();
        }

    }
    void Fire()
    {
        for (int i = 0; i < SpitList.Count; i++)
        {
            if (!SpitList[i].activeInHierarchy)
            {
                SpitList[i].transform.position = SpiterMouth.transform.position;
                SpitList[i].transform.rotation = SpiterMouth.transform.rotation;
                SpitList[i].SetActive(true);
                Rigidbody tempRigidBodySpit = SpitList[i].GetComponent<Rigidbody>();
                StartCoroutine(SimulateProjectile(i));
                break;
            }
        }
    }
    IEnumerator SimulateProjectile(int i)
    {
        yield return new WaitForSeconds(0.0f);
        SpitList[i].transform.position = SpiterMouth.transform.position + new Vector3(0, 0.0f, 0);
        Vector3 Player = playerTransform.position + new Vector3(0, -playerTransform.position.y, 0);
        float target_Distance = Vector3.Distance(SpitList[i].transform.position, Player);
        float Spit_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        float Vx = Mathf.Sqrt(Spit_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(Spit_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        float flightDuration = target_Distance / Vx;

        SpitList[i].transform.rotation = Quaternion.LookRotation(Player - SpitList[i].transform.position);
        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            SpitList[i].transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}
