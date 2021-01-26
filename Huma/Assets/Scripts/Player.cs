using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isDead = false;
    protected float hp;
    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp <= 0)
            {
                isDead = true;
            }
        }
    }

    public float HpChanged(float damage)
    {
        HP -= damage;
        return HP;
    }

    public float Atk; // 공격력
    public float AtkTime; // 공격 속도
    public float AtkRange; // 공격 사거리
    public int AtkCount; // 한번에 공격 가능한 수
    public float moveSpeed; // 이동 속도
}
