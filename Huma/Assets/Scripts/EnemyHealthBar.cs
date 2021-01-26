//**********************************
// EDITOR : KANG DaHye
// LAST EDITED DATE : 2021.01.25
// Scrit Purpose : Enemy HealthBar
//**********************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Ex_Health health;
    public Image healthbarImage;
    public Transform healthbarPivot;
    public float activeTime = 5f;
    private float currentHp;
    private float timer = 0f;

    public void Start()
    {
        currentHp = health.maxHealth;
        healthbarPivot.gameObject.SetActive(false);
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (health.currentHealth != currentHp)
        {
            timer = 0;
            healthbarPivot.gameObject.SetActive(true);
            StartCoroutine(ChangeHealth());
            currentHp = health.currentHealth;
        }
        if (timer >= activeTime)
        {
            healthbarPivot.gameObject.SetActive(false);
        }
    }
    private IEnumerator ChangeHealth()
    {
        float changeHp = healthbarImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            healthbarImage.fillAmount = Mathf.Lerp(changeHp, health.currentHealth / health.maxHealth, elapsed / 0.5f);
            yield return null;
        }
        //healthbarImage.fillAmount = health.currentHealth / health.maxHealth;
    }

}
