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
    private string[] expArr;

    public enum PlayerID { kim = 0 }
    public PlayerID playerID;
    public int ID; // Character ID
    public string characterName;
    public float damage;
    public float hp;
    public float attackSpeed;
    public float attackRange;
    public float attackRadius;
    public float criticalProb;
    public float criticalDamage;
    public float bloodSucking;
    public float moveSpeed;
    public int NumberOfTargets;
    public float skillCoolTime;


    #region Exp
    [HideInInspector]
    public List<float> ExpList = new List<float>();
    #endregion

    public bool isLoaded = false;

    /// <summary>
    /// Load Character Data(EXP,Status) from Server 
    /// </summary>
    /// <param name="ID">Player's Character ID in DB</param>
    public void LoadingCharacterData(int ID)
    {
        isLoaded = false;
        ID = (int)playerID;
        StartCoroutine(PlayerDataGet("http://220.127.167.244:8080/WinterPenguin_Huma/CharactersDB.php",ID));
        StartCoroutine(ExpDataGet("http://220.127.167.244:8080/WinterPenguin_Huma/PlayerExpDB.php", ID));
    }

    /// <summary>
    /// Get Exp value from Server
    /// </summary>
    /// <param name="_url">url of Player's EXP php file</param>
    /// <param name="_index">ID of Player Character</param>
    /// <returns></returns>
    private IEnumerator ExpDataGet(string _url, int _index)
    {
        UnityWebRequest www = UnityWebRequest.Get(_url);
        yield return www.SendWebRequest();

        if (www.error != null)
            Debug.Log(www.error.ToString());

        expArr = www.downloadHandler.text.Split(';');

        for(int i = 2; i < GetDataValue(expArr[_index]).Length; i++) // ID와 Name 을 건너뛰고 lv.2 경험치부터 가져오기
        {
            ExpList.Add(Convert.ToSingle(GetDataValue(expArr[_index])[i]));
        }

    }

    /// <summary>
    /// Get Player status value from Server
    /// </summary>
    /// <param name="_characterUrl">url of Player's status php file</param>
    /// <param name="_index">ID of Player Character</param>
    /// <returns></returns>
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
        attackRange = Convert.ToSingle(GetDataValue(characterArr[_index], "AttackRange:"));
        attackRadius = Convert.ToSingle(GetDataValue(characterArr[_index], "AttackRadius:"));
        criticalProb = Convert.ToSingle(GetDataValue(characterArr[_index], "CriticalProbability:"));
        criticalDamage = Convert.ToSingle(GetDataValue(characterArr[_index], "CriticalDamage:"));
        bloodSucking = Convert.ToSingle(GetDataValue(characterArr[_index], "BloodSucking:"));
        moveSpeed = Convert.ToSingle(GetDataValue(characterArr[_index], "MoveSpeed:"));
        NumberOfTargets = Convert.ToInt32(GetDataValue(characterArr[_index], "NumberOfTargets:"));
        skillCoolTime = Convert.ToSingle(GetDataValue(characterArr[_index], "SkillCoolTime:"));
        isLoaded = true;
    }

    /// <summary>
    /// Substring Each value of php file
    /// </summary>
    /// <param name="data">One part of DB file</param>
    /// <param name="index">Wanted Collumn line number of DB</param>
    /// <returns></returns>
    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index)+index.Length) ;
        if(value.Contains("|"))
            value = value.Remove(value.IndexOf("|"));
        return value;
    }

    /// <summary>
    /// Substring Each value of php file
    /// </summary>
    /// <param name="data">One part of DB file</param>
    /// <returns></returns>
    string[] GetDataValue(string data)
    {
        string[] value = data.Split('|');
        return value;
    }
}
