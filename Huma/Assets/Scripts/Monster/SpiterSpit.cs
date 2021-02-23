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

    MonsterBase SpitDamage;
    protected float damage;


    void Start()
    {
        SpitDamage = GameObject.Find("Monster").GetComponent<MonsterBase>();
        //damage = SpitDamage.Damage;
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
            /*
                        Player player = new Player();
                        player.HpChanged(damage);
            */
        }
    }
}

