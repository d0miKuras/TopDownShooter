using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100.0f;
    public Slider slider;
    public void SetMaxHealth(float value)
    {
        maxHealth = value;
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetHealth(float value)
    {
        slider.value = value;
    }
}
