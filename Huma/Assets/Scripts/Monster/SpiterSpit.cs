//************************************************
// EDITOR : JNE
// LAST EDITED DATE : 2020.02.22
// Script Purpose : Monster_spit damage
//******************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiterSpit : MonoBehaviour
{
    [SerializeField] private DBManager_Monster MonsterData;
    Player player;

    private float damage;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        MonsterData = GameObject.Find("DBManager").GetComponent<DBManager_Monster>();

        damage = MonsterData.monsterDB[2].damage;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "wall")
        {
            gameObject.SetActive(false);
        }
        else if (collision.tag == "Player")
        {
            gameObject.SetActive(false);          
            player.HpChanged(-damage);
        }
    }
}

