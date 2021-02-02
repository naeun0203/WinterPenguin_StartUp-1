//************************************************
// EDITOR : JNE
// LAST EDITED DATE : 2020.01.27
// Script Purpose : Monster_Spiter Spit
//******************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiterSpit : MonoBehaviour
{ 
    //public float damage = 1.0f;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Wall")
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

