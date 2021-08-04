using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkText : MonoBehaviour
{
    TextMeshProUGUI blinkText;
    //Text blinkText;
    public float time = 2.0f;
    void Start()
    {
        blinkText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeInText());
    }
    void Update()
    {

    }
    public IEnumerator FadeInText()
    {
        blinkText.color = new Color(blinkText.color.r, blinkText.color.g, blinkText.color.b, 0);
        while (blinkText.color.a < 1.0f)
        {
            blinkText.color = new Color(blinkText.color.r, blinkText.color.g, blinkText.color.b, blinkText.color.a + (Time.deltaTime/time));
            yield return null;
        }
        StartCoroutine(FadeOutText());
    }
    public IEnumerator FadeOutText()
    {
        blinkText.color = new Color(blinkText.color.r, blinkText.color.g, blinkText.color.b, 1);
        while (blinkText.color.a > 0.0f)
        {
            blinkText.color = new Color(blinkText.color.r, blinkText.color.g, blinkText.color.b, blinkText.color.a - (Time.deltaTime / time));
            yield return null;
        }
        StartCoroutine(FadeInText());
    }
}
