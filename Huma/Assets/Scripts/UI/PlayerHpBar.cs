using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public float maxHp;//
    private Transform cam;
    
    private DBManager_Player PlayerDB;
    [SerializeField]
    private Player player;

    [SerializeField]
    private Image hpbarImage;
    [SerializeField]
    private Transform hpBarPivot;
    [SerializeField]
    private float currentHp;
    [SerializeField]
    private float lerpSpeed = 0.5f;

    private Vector3 lookPosition;
    //Player player = new Player();
    public void Start()
    {
        PlayerDB = GameObject.Find("DBManager").GetComponent<DBManager_Player>();
        cam = Camera.main.transform;

        player.HP = PlayerDB.hp;
        maxHp = player.HP;
        //maxHp = PlayerDB.hp;
        currentHp = maxHp;

        hpbarImage.fillAmount = 1;//
        hpBarPivot.gameObject.SetActive(true);
    }
    private void Update()
    {
        currentHp = player.HP;
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            player.HpChanged(10);
        }
        if (maxHp != currentHp)
        {
            hpbarImage.fillAmount = Mathf.Lerp(hpbarImage.fillAmount, currentHp / maxHp, Time.deltaTime * lerpSpeed);
        }
    }
    private void LateUpdate()
    {
        lookPosition = new Vector3(hpBarPivot.transform.position.x, cam.position.y, cam.position.z);
        hpBarPivot.transform.LookAt(lookPosition);
    }
}
