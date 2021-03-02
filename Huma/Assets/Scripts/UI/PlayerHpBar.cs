using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
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
    private float maxHp;
    [SerializeField]
    private float lerpSpeed = 0.5f;

    private Vector3 lookPosition;
    public void Start()
    {
        PlayerDB = GameObject.Find("DBManager").GetComponent<DBManager_Player>();
        cam = Camera.main.transform;

        //maxHp = player.HP;
        //currentHp = maxHp;

        hpbarImage.fillAmount = 1;
        hpBarPivot.gameObject.SetActive(true);
        StartCoroutine(DataSet());
    }

    private IEnumerator DataSet()
    {
        bool DataLoading = true;

        while (DataLoading)
        {
            if (PlayerDB.isLoaded)
            {
                maxHp = PlayerDB.hp;

                DataLoading = false;
            }
            yield return null;
        }
        currentHp = maxHp;
        yield return null;
    }
    private void Update()
    {
        currentHp = player.HP;

/*        if (Input.GetKeyDown(KeyCode.P))
        {
            player.HpChanged(10);
        }*/
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
