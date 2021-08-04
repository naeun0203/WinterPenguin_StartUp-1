using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum MonsterType { Zombie, DevilDog, Spiter, Tanker };

    [SerializeField]
    public MonsterType monsterType;

    public AudioClip Attack;
    public AudioClip Hit;
    public AudioClip Dead;
}
public class MonsterSound : MonoBehaviour
{
    MonsterBase monbase;
    public static MonsterSound instance;
    [ArrayElementTitle("monsterType")]
    [Header("SoundInput")]
    [SerializeField] Sound[] Sound;

    [Header("SoundPlyaer")]
    [SerializeField] AudioSource[] SoundPlayer;
    // Start is called before the first frame update
    void Start()
    {
        monbase = GameObject.Find("Monster").GetComponent<MonsterBase>();
        instance = this;
    }
    public void PlaySE(string _soundName)
    {
        for(int i = 0; i < Sound.Length; i++)
        {
            if(Sound[i].monsterType.ToString() == monbase.CurrentTribe.ToString())
            {
                if (_soundName == Sound[i].Attack.name)
                {
                    for (int x = 0; x < SoundPlayer.Length; x++)
                    {
                        if (!SoundPlayer[x].isPlaying)
                        {
                            SoundPlayer[x].clip = Sound[i].Attack;
                            SoundPlayer[x].Play();
                            return;
                        }
                    }
                    return;
                }
            }
        }
    }
}
