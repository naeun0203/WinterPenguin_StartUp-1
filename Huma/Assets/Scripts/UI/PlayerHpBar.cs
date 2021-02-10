using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    private float maxHp;//
    private Transform cam;


    public GameObject Player;
    public Image healthbarImage;
    public Transform hpbarPivot;
    //public float activeTime = 5f;
    private float currentHp;

    private Vector3 lookPosition;

    public void Start()
    {
        cam = Camera.main.transform;

        maxHp = Player.GetComponent<Player>().HP;
        currentHp = maxHp;
        hpbarPivot.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (maxHp != currentHp)
        {
            StartCoroutine(ChangeHealth());
            currentHp = maxHp;
        }
    }
    private IEnumerator ChangeHealth()
    {
        float changeHp = healthbarImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            healthbarImage.fillAmount = Mathf.Lerp(changeHp, currentHp / maxHp, elapsed / 0.5f);
            yield return null;
        }
        //healthbarImage.fillAmount = health.currentHealth / health.maxHealth;
    }
    private void LateUpdate()
    {
        lookPosition = new Vector3(hpbarPivot.transform.position.x, cam.position.y, cam.position.z);
        hpbarPivot.transform.LookAt(lookPosition);
    }
}
