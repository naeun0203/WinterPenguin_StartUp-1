using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void PlaySceneChange()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
