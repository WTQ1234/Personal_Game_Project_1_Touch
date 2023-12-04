using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIshanshuo : MonoBehaviour
{
    private float originalAlpha;
    float minAlpha = 0;
    float maxAlpha = 1;
    public float frequency = 5;
    private void Start()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        originalAlpha = canvasGroup.alpha;
    }

    private void Update()
    {
        float sinValue = Mathf.Sin(Time.time * frequency);
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (sinValue + 1) / 2);
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = alpha;
    }
}
