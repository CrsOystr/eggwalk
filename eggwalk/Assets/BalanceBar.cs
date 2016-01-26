using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BalanceBar : MonoBehaviour
{

    [SerializeField] private GameObject indicatorParent;
    [SerializeField] private Image indicatorFill;
    [SerializeField] private Image flashingGague;
    [SerializeField] private float flashingFrequency;
    [SerializeField] private Gradient indicatorGradient;
    [SerializeField] private Gradient gaugeGradient;
    
    // Use this for initialization
    void Start () {
        if (indicatorGradient.colorKeys.Length > 0) {
            indicatorFill.color = indicatorGradient.colorKeys[0].color;
            this.flashingGague.color = gaugeGradient.Evaluate(0.0f);
        }
	}

    /**
     * setIndicatorColor - Set the color of the indicator based on the current
     *                     IndicatorColor gradient from 0 to 1
     * Input:
     *     gradValue: Number from 0 - 1 indicating color from gradient
     */
    public void setIndicatorColor(float gradValue)
    {
        gradValue = Mathf.Abs(gradValue);
        gradValue = Mathf.Clamp(gradValue, 0.0f, 1.0f);

        indicatorFill.color = this.indicatorGradient.Evaluate(gradValue);
    }

    public void setIndicatorColorFromAngle(float currentAngle, float killAngle)
    {
        currentAngle = Mathf.Abs(currentAngle);

        float gradValue = (killAngle - currentAngle) / killAngle;
        gradValue = Mathf.Clamp(gradValue, 0.0f, 1.0f);
        indicatorFill.color = this.indicatorGradient.Evaluate(gradValue);
    }

    public void flashGauge()
    {
        float t = Mathf.PingPong(Time.time / flashingFrequency, 1.0f);
        this.flashingGague.color = gaugeGradient.Evaluate(t);
    }

    public void resetGague()
    {
        this.flashingGague.color = gaugeGradient.Evaluate(0.0f);
    }

    public GameObject IndicatorParent
    {
        get { return this.indicatorParent; }
    }
}
