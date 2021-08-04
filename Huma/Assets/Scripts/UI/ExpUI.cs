using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpUI : MonoBehaviour
{
    private DBManager_Player PlayerDB;
    [SerializeField]
    private Player player;

    [SerializeField]
    private Image expImage;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private float currentExp = 0;
    [SerializeField]
    private float maxExp;

    [SerializeField]
    private float lerpSpeed = 0.5f;
    public void Start()
    {
        PlayerDB = GameObject.Find("DBManager").GetComponent<DBManager_Player>();

        expImage.fillAmount = 0;
        StartCoroutine(DataSet());
    }
    private IEnumerator DataSet()
    {
        bool DataLoading = true;

        while (DataLoading)
        {
            if (PlayerDB.isLoaded)
            {
                maxExp = PlayerDB.ExpList[player.level];
                
                DataLoading = false;
            }
            yield return null;
        }
        //currentExp = maxExp;
        yield return null;
    }
    private void Update()
    {
        currentExp = player.EXP;

        expImage.fillAmount = Mathf.Lerp(expImage.fillAmount, currentExp / maxExp, Time.deltaTime * lerpSpeed);
        
        levelText.text = string.Format("Level {0}", player.level);
    }
}
