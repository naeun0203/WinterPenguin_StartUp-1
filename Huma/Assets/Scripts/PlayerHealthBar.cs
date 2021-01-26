//**********************************
// EDITOR : KANG DaHye
// LAST EDITED DATE : 2021.01.25
// Scrit Purpose : Player HealthBar
//**********************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Ex_Health health;
    public Image healthbarImage;
    public Transform healthbarPivot;
    private float currentHp;

    public void Start()
    {
        healthbarPivot.gameObject.SetActive(true);
        currentHp = health.maxHealth;
    }

    private void Update()
    {
        if(currentHp != health.currentHealth)
        {
            StartCoroutine(ChangeHealth());
            currentHp = health.currentHealth;
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
    }
}
