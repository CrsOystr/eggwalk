using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UISystem : MonoBehaviour
{
    [SerializeField] private Text LifeText;
    [SerializeField] private GameObject RestartButton;
    [SerializeField] private GameObject BalanceTriangle;
    [SerializeField] private Image HurtImageMask;
    [SerializeField] private Image TurnRightSignal;
    [SerializeField] private Image TurnLeftSignal;
    [SerializeField] private Text ObjectiveListTextBox;
    private bool HurtMaskisVisible;
    private bool TurnRightSignalIsVisible;
    private bool TurnLeftSignalIsVisible;

    void Start()
    {
        HurtMaskisVisible = false;
        TurnRightSignalIsVisible = false;
        TurnLeftSignalIsVisible = false;

        setImageAlpha(ref HurtImageMask, 0.0f);
        setImageAlpha(ref TurnRightSignal, 0.0f);
        setImageAlpha(ref TurnLeftSignal, 0.0f);
    }

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

        if (TurnRightSignalIsVisible)
        {
            float alpha = TurnRightSignal.color.a - 0.02f;

            if (alpha > 0)
            {
                setImageAlpha(ref TurnRightSignal, alpha);
            } 
            else
            {
                setImageAlpha(ref TurnRightSignal, 0.0f);
                TurnRightSignalIsVisible = false;
            }
        }

        if (TurnLeftSignalIsVisible)
        {
            float alpha = TurnLeftSignal.color.a - 0.02f;

            if (alpha > 0)
            {
                setImageAlpha(ref TurnLeftSignal, alpha);
            }
            else
            {
                setImageAlpha(ref TurnLeftSignal, 0.0f);
                TurnLeftSignalIsVisible = false;
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

    public void setVisibilityToLives(bool visible)
    {
        LifeText.color = (visible) ? new Color(1.0f, 1.0f, 1.0f, 1.0f) :
            new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    public void returnButtonAction(string level)
    {
        Application.LoadLevel(level);
    }

    public void setRotationToBalanceBar(float deltaRotation)
    {
        BalanceTriangle.transform.localEulerAngles = Vector3.forward * deltaRotation;
    }

    public void showHurtMask(bool visible)
    {
        HurtImageMask.color = new Color(HurtImageMask.color.r, HurtImageMask.color.g, HurtImageMask.color.b, 1.0f);
        HurtMaskisVisible = true;
    }

    public void showRightSignalImage(bool visible)
    {
        setImageAlpha(ref TurnRightSignal, (visible) ? 1.0f : 0.0f );
        TurnRightSignalIsVisible = visible;
    }

    public void showLeftSignalImage(bool visible)
    {
        setImageAlpha(ref TurnLeftSignal, (visible) ? 1.0f : 0.0f );
        TurnLeftSignalIsVisible = visible;
    }

    private void setImageAlpha(ref Image image, float alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }
    
    public void setObjectiveListText(string text)
    {
        ObjectiveListTextBox.text = text;
    } 
}
