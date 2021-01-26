//**********************************
// EDITOR : KANG DaHye
// LAST EDITED DATE : 2021.01.25
// Scrit Purpose : Example Health
//**********************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_Health : MonoBehaviour
{
    public float speed = 5f;
    private  Rigidbody rig;

    public float maxHealth = 10f;
    public float currentHealth;
    public event Action <float> OnHealthChanged = delegate { };
    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }
    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(inputX, 0, inputY);
        velocity *= speed;
        rig.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.C))
        {
            changeHp(-10);
        }
    }
    public void changeHp(int value)
    {
        currentHealth += value;

        //currentHealth = (float)currentHealth / (float)maxHealth;
        OnHealthChanged(currentHealth);
    }
}
