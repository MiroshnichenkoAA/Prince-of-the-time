using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CD_Shadow : MonoBehaviour
{
    public Slider shadowCDSlider;
    public Gradient shadowCDGradient;
    public Image shadowCDFill;

    public void SetMaxShadowCDTime(float shadowLength)
    {
        shadowCDSlider.maxValue = shadowLength;
        shadowCDSlider.value = shadowLength;
        shadowCDFill.color = shadowCDGradient.Evaluate(1f);
    }
    public void SetShadowCDTime(float shadowCounter)
    {
        shadowCDSlider.value = shadowCounter;
        shadowCDFill.color = shadowCDGradient.Evaluate(shadowCDSlider.normalizedValue);

    }

}
