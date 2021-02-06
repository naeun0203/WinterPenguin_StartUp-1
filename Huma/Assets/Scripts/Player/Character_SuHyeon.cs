using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_SuHyeon : Player
{
    [SerializeField] private DBManager_Player PlayerDB;
    private CharacterController characterCont;

    private void Start()
    {
        PlayerDB = GameObject.Find("DBManager").GetComponent<DBManager_Player>();
        characterCont = GetComponent<CharacterController>();
        PlayerDB.LoadingCharacterData(0);
        StartCoroutine(DataSet());
        
    }

    private IEnumerator DataSet()
    {
        yield return PlayerDB.isLoaded;
        HP = PlayerDB.hp;
        AtkDamage = PlayerDB.damage;
        AtkSpeed = PlayerDB.attackSpeed;
        AtkRange = PlayerDB.attackRange;
        AtkRadius = PlayerDB.attackRadius;
        CriticalProb = PlayerDB.criticalProb;
        CriticalDamage = PlayerDB.criticalDamage;
        BloodSucking = PlayerDB.bloodSucking;
        MoveSpeed = PlayerDB.moveSpeed;
        AtkCount = PlayerDB.NumberOfTargets;
        SkillCoolTime = PlayerDB.skillCoolTime;

        characterCont.speed = MoveSpeed;

        yield return null;
    }

}
