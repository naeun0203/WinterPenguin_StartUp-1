using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isDead = false;
    private float hp;
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

    [SerializeField] protected float AtkDamage; // 공격력
    [SerializeField] protected float AtkSpeed; // 공격 속도
    [SerializeField] protected float AtkRange; // 공격 사거리
    [SerializeField] protected int AtkCount; // 한번에 공격 가능한 수
    [SerializeField] protected float MoveSpeed; // 이동 속도
    [SerializeField] protected int AtkRadius;
    [SerializeField] protected float CriticalProb;
    [SerializeField] protected float CriticalDamage;
    [SerializeField] protected float BloodSucking;
    [SerializeField] protected int SkillCoolTime;
    
}
