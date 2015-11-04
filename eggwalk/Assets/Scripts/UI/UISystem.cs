using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISystem : MonoBehaviour
{
    [SerializeField] private Text LifeText;
    [SerializeField] private GameObject RestartButton;
    [SerializeField] private GameObject BalanceTriangle;
    [SerializeField] private Image HurtImageMask;
    private bool HurtMaskisVisible;
    private float TimeElapsed;
    private float MaxTime;

    void Update()
    {
        if (HurtMaskisVisible)
        {
            float alpha = HurtImageMask.color.a - 0.01f;

            if (HurtImageMask.color.a > 0.01f)
            {
                HurtImageMask.color = new Color(HurtImageMask.color.r, HurtImageMask.color.g, HurtImageMask.color.b, alpha);
            }
            else
            {
                HurtImageMask.color = new Color(HurtImageMask.color.r, HurtImageMask.color.g, HurtImageMask.color.b, 0.0f);
                HurtMaskisVisible = false;
            }
        }
    }

    public void setCurrentLives(int currentLives, int maxLives)
    {
        LifeText.text = (currentLives > 0) ? "Lives " + currentLives + "/" + maxLives :
                                             "Lives " + 0 + "/" + maxLives;
    }

    public void setVisibilityToRestart(bool isVisible)
    {
        RestartButton.SetActive(true);
    }

    public void returnButtonAction(string level)
    {
        Application.LoadLevel(level);
    }

    public void setRotationToBalanceBar(float deltaRotation)
    {
        BalanceTriangle.transform.localEulerAngles = Vector3.forward * deltaRotation * -1.0f;
    }

    public void showHurtMask(bool visible)
    {
        HurtImageMask.color = new Color(HurtImageMask.color.r, HurtImageMask.color.g, HurtImageMask.color.b, 1.0f);
        HurtMaskisVisible = true;
    }
}
