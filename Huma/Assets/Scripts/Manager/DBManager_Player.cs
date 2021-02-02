using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager_Player : MonoBehaviour
{
    private string[] characters;

    private void Start()
    {
        StartCoroutine(DataGet("http://localhost/WinterPenguin_Huma/CharactersDB.php"));
    }

    private IEnumerator DataGet(string _url)
    {
        UnityWebRequest www = UnityWebRequest.Get(_url);
        yield return www.SendWebRequest();
        if (www.error != null)
        {
            Debug.Log(www.error.ToString());
        }

        characters = www.downloadHandler.text.Split(';');
        Debug.Log(GetDataValue(characters[0], "Damage:"));
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index)+index.Length) ;
        value = value.Remove(value.IndexOf("|"));
        return value;
    }
}
