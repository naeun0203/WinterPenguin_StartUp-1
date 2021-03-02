/*
 * 
 * EDITOR : KIM Ji hun 
 * Last Edit : 2021.2.27
 * Script Purpose : All characeter's parent Script
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Player : MonoBehaviour
{
    public bool activatingPlayer; // Is current Playing character can move or activate now?
    public bool isDead = false; // Is Character dead?
    [SerializeField]  private float hp; // Player HP
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

    abstract public float EXP { get; set; }
    public int level;
    public float MaxHP; // 최대 체력
    public float AtkDamage; // 공격력
    public float AtkSpeed; // 공격 속도
    public float AtkRange; // 공격 사거리
    public int AtkCount; // 한번에 공격 가능한 수
    public float MoveSpeed; // 이동 속도
    public int AtkRadius;
    public float CriticalProb;
    public float CriticalDamage;
    public float BloodSucking;
    public int SkillCoolTime;
    public float Exp;
   
}
