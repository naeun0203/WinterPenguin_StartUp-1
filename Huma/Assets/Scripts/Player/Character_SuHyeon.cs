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
        PlayerDB.LoadingCharacterData(0);
        characterCont = GetComponent<CharacterController>();
        StartCoroutine(DataSet());
    }
   
    

    private IEnumerator DataSet()
    {
        bool DataLoading = true;

        while (DataLoading)
        {
            if (PlayerDB.isLoaded)
            {
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

                DataLoading = false;
            }
            yield return null;
        }

        characterCont.speed = MoveSpeed;
        yield return null;
    }

}
