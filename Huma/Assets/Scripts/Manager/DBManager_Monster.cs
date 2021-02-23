//************************************************
// EDITOR : JNE
// LAST EDITED DATE : 2020.02.22
// Script Purpose : Monster_DB
//******************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager_Monster : MonoBehaviour
{
    public string[] Monster;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        WWW MonsterData = new WWW("http://220.127.167.244:8080/WinterPenguin_Huma/MonsterData.php");
        yield return MonsterData;
        string MonsterDataString = MonsterData.text;
        print(MonsterDataString);
        Monster = MonsterDataString.Split(';');
        print(GetDataValue(Monster[0], "name: "));
    }

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
