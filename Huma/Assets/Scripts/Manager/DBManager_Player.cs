/*
 * 
 * EDITOR : KIM Ji hun 
 * Last Edit : 2021.2.17
 * Script Purpose : Setting Character(Player)'s Database
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class DBManager_Player : MonoBehaviour
{
    private string[] characters;

    public int ID; // Character ID
    public string characterName;
    public float damage;
    public float hp;
    public float attackSpeed;
    public int attackRange;
    public int attackRadius;
    public float criticalProb;
    public float criticalDamage;
    public float bloodSucking;
    public int moveSpeed;
    public int NumberOfTargets;
    public int skillCoolTime;

    public bool isLoaded = false;

    public void LoadingCharacterData(int ID)
    {
        isLoaded = false;
        StartCoroutine(DataGet("http://220.127.167.244:8080//WinterPenguin_Huma/CharactersDB.php", ID));

    }

    private IEnumerator DataGet(string _url, int _index)
    {
        UnityWebRequest www = UnityWebRequest.Get(_url);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error.ToString());
        }

        characters = www.downloadHandler.text.Split(';');

        characterName = GetDataValue(characters[_index], "Name:");
        hp = Convert.ToSingle(GetDataValue(characters[_index], "HP:"));
        damage = Convert.ToSingle(GetDataValue(characters[_index], "Damage:"));
        attackSpeed = Convert.ToSingle(GetDataValue(characters[_index], "AttackSpeed:"));
        attackRange = Convert.ToInt32(GetDataValue(characters[_index], "AttackRange:"));
        attackRadius = Convert.ToInt32(GetDataValue(characters[_index], "AttackRadius:"));
        criticalProb = Convert.ToSingle(GetDataValue(characters[_index], "CriticalProbability:"));
        criticalDamage = Convert.ToSingle(GetDataValue(characters[_index], "CriticalDamage:"));
        bloodSucking = Convert.ToSingle(GetDataValue(characters[_index], "BloodSucking:"));
        moveSpeed = Convert.ToInt32(GetDataValue(characters[_index], "MoveSpeed:"));
        NumberOfTargets = Convert.ToInt32(GetDataValue(characters[_index], "NumberOfTargets:"));
        skillCoolTime = Convert.ToInt32(GetDataValue(characters[_index], "SkillCoolTime:"));
        isLoaded = true;

    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index)+index.Length) ;
        if(value.Contains("|"))
            value = value.Remove(value.IndexOf("|"));
        return value;
    }
}
