using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform indicator;

    private float maxValue;
   
    public void Init(float maxValue)
    {
        this.maxValue = maxValue;
    }

    public void UpdateIndicator(float value)
    {
        indicator.localScale = new Vector3(value / maxValue, 1, 1);
    }
}
