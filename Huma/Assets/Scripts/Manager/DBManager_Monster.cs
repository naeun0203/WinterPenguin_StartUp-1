//************************************************
// EDITOR : JNE
// LAST EDITED DATE : 2020.02.24
// Script Purpose : Monster_DB
//******************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;

[System.Serializable]
public struct MonsterDB
{
    public enum MonsterType { Zombie, DevilDog, Spiter, Tanker };

    [SerializeField]
    public MonsterType monsterType;

    [SerializeField]
    public int ID;
    [SerializeField]
    public string name;
    [SerializeField]
    public float hp, damage, attackSpeed, attackRange, attackRadius, bloodSucking, moveSpeed, NumberOfTargets, skillCoolTime, EXP;
}

public class DBManager_Monster : MonoBehaviour
{
    [ArrayElementTitle("monsterType")]
    [SerializeField]
    public MonsterDB[] monsterDB;
    public string[] Monster;

    IEnumerator Start()
    {
        WWW monsterData = new WWW("http://220.127.167.244:8080/WinterPenguin_Huma/MonsterData.php");
        yield return monsterData;
        string monsterDataString = monsterData.text;
        Monster = monsterDataString.Split(';');

        for (int i = 0; i < monsterDB.Length; i++)
        {
            monsterDB[i].ID = Convert.ToInt32(GetDataValue(Monster[i], "ID:"));
            monsterDB[i].name = GetDataValue(Monster[i], "Name:");
            monsterDB[i].hp = Convert.ToSingle(GetDataValue(Monster[i], "HP:"));
            monsterDB[i].damage = Convert.ToSingle(GetDataValue(Monster[i], "Damage:"));
            monsterDB[i].attackSpeed = Convert.ToSingle(GetDataValue(Monster[i], "AttackSpeed:"));
            monsterDB[i].attackRange = Convert.ToSingle(GetDataValue(Monster[i], "AttackRange(m):"));
            monsterDB[i].attackRadius = Convert.ToSingle(GetDataValue(Monster[i], "AttackRadius(m):"));
            monsterDB[i].bloodSucking = Convert.ToSingle(GetDataValue(Monster[i], "BloodSucking(%):"));
            monsterDB[i].moveSpeed = Convert.ToSingle(GetDataValue(Monster[i], "MoveSpeed:"));
            monsterDB[i].NumberOfTargets = Convert.ToSingle(GetDataValue(Monster[i], "NumberOfTargets:"));
            monsterDB[i].skillCoolTime = Convert.ToSingle(GetDataValue(Monster[i], "SkillCoolTime:"));
            monsterDB[i].EXP = Convert.ToSingle(GetDataValue(Monster[i], "EXP:"));
        }

    }

    // Start is called before the first frame update

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if(value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }

}
