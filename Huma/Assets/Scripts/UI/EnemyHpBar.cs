//**********************************
// EDITOR : KANG DaHye
// LAST EDITED DATE : 2021.02.08
// Scrit Purpose : Enemy HealthBar
//**********************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    private float maxHp;
    private Transform cam;

    public Image healthbarImage;
    public Transform hpbarPivot;
    public float activeTime = 5f;
    private float currentHp;
    private float timer = 0f;

    private Vector3 lookPosition;
    public void Start()
    {
        cam = Camera.main.transform;

        currentHp = maxHp;
        hpbarPivot.gameObject.SetActive(false);
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (maxHp != currentHp)
        {
            timer = 0;
            hpbarPivot.gameObject.SetActive(true);
            StartCoroutine(ChangeHealth());
            currentHp = maxHp;
        }
        if (timer >= activeTime)
        {
            hpbarPivot.gameObject.SetActive(false);
        }
    }
    private IEnumerator ChangeHealth()
    {
        float changeHp = healthbarImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            healthbarImage.fillAmount = Mathf.Lerp(changeHp, maxHp / maxHp, elapsed / 0.5f);
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
