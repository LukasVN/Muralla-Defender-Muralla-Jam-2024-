using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private Image healthBar;
    private Color green;
    private Color red;
    void Start()
    {
        slider = GetComponent<Slider>();
        healthBar = transform.GetChild(0).GetComponent<Image>();
        green = healthBar.color;
        red = Color.red;
    }

    void Update()
    {
        float t = slider.value / slider.maxValue;
        healthBar.color = Color.Lerp(red, green, t);
    }
}
