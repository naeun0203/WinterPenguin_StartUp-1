using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainMonsterUI : MonoBehaviour
{
    [SerializeField]
    public WaveSpawner Spawner;
    public TextMeshProUGUI MonsterText;

    private void Start()
    {
        //Spawner = GameObject.Find("Spawn").GetComponent<WaveSpawner>();
    }
    private void Update()
    {
        MonsterText.text = string.Format("남은 몬스터 수: {0}", 1);
    }
}