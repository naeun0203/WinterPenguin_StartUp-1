/*
 * 
 * EDITOR : KIM Ji hun 
 * Last Edit : 2021.2.19
 * Script Purpose : All characeter's parent Script
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool activatingPlayer; // Is current Playing character can move or activate now?
    public bool isDead = false; // Is Character dead?
    private float hp; // Player HP
    public float HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if (hp > MaxHP)
            {
                hp = MaxHP;
            }
            if (hp <= 0)
            {
                isDead = true;
            }
        }
    }

    public virtual IEnumerator Attack() { yield return null; }

    public float HpChanged(float damage)
    {
        HP += damage;
        return HP;
    }

    [SerializeField] protected float MaxHP; // 최대 체력
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
