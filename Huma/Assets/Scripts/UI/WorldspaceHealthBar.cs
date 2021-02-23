using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldspaceHealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public Transform healthBatPivot;

    void Update()
    {
        //healthBarImage.fillAmount = Mathf.Lerp(healthBarImage.fillAmount, currentHp / maxHp, Time.deltaTime * lerpSpeed);

        healthBatPivot.LookAt(Camera.main.transform.position);
    }
}
