using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shadow_Timer : MonoBehaviour
{
    public Slider shadowSlider;
    public Gradient shadowGradient;
    public Image shadowFill;

    public void SetMaxShadowTime(float shadowLength)
    {
        shadowSlider.maxValue = shadowLength;
        shadowSlider.value = shadowLength;
        shadowFill.color = shadowGradient.Evaluate(1f);
    }
    public void SetShadowTime(float shadowCounter)
    {
        shadowSlider.value = shadowCounter;
        shadowFill.color = shadowGradient.Evaluate(shadowSlider.normalizedValue);

    }
   
}
