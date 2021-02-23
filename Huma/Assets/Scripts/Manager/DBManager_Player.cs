/*
 * 
 * EDITOR : KIM Ji hun 
 * Last Edit : 2021.2.17
 * Script Purpose : Setting character(Player)'s Database
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class DBManager_Player : MonoBehaviour
{
    private string[] characterArr;

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

    #region Exp
    [HideInInspector]
    public List<float> ExpList = new List<float>();
    #endregion

    public bool isLoaded = false;

    public void LoadingCharacterData(int ID)
    {
        isLoaded = false;
        StartCoroutine(PlayerDataGet("http://220.127.167.244:8080//WinterPenguin_Huma/CharactersDB.php",ID));

    }

    private IEnumerator PlayerDataGet(string _characterUrl, int _index)
    {
        UnityWebRequest charData = UnityWebRequest.Get(_characterUrl);
        yield return charData.SendWebRequest();
        if (charData.error != null)
        {
            Debug.Log(charData.error.ToString());
        }

        characterArr = charData.downloadHandler.text.Split(';');

        characterName = GetDataValue(characterArr[_index], "Name:");
        hp = Convert.ToSingle(GetDataValue(characterArr[_index], "HP:"));
        damage = Convert.ToSingle(GetDataValue(characterArr[_index], "Damage:"));
        attackSpeed = Convert.ToSingle(GetDataValue(characterArr[_index], "AttackSpeed:"));
        attackRange = Convert.ToInt32(GetDataValue(characterArr[_index], "AttackRange:"));
        attackRadius = Convert.ToInt32(GetDataValue(characterArr[_index], "AttackRadius:"));
        criticalProb = Convert.ToSingle(GetDataValue(characterArr[_index], "CriticalProbability:"));
        criticalDamage = Convert.ToSingle(GetDataValue(characterArr[_index], "CriticalDamage:"));
        bloodSucking = Convert.ToSingle(GetDataValue(characterArr[_index], "BloodSucking:"));
        moveSpeed = Convert.ToInt32(GetDataValue(characterArr[_index], "MoveSpeed:"));
        NumberOfTargets = Convert.ToInt32(GetDataValue(characterArr[_index], "NumberOfTargets:"));
        skillCoolTime = Convert.ToInt32(GetDataValue(characterArr[_index], "SkillCoolTime:"));
        isLoaded = true;
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index)+index.Length) ;
        if(value.Contains("|"))
            value = value.Remove(value.IndexOf("|"));
        return value;
    }

    private List<float> GetExpValue()
    {
        //ExpList = 
        return ExpList;
    }
}
