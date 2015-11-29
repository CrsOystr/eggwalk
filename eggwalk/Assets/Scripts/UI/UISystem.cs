using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UISystem : MonoBehaviour
{
    [SerializeField] private GameObject HUDElements;
    [SerializeField] private GameObject GameOverElements;
    [SerializeField] private Text LifeText;
    [SerializeField] private GameObject RestartButton;
    [SerializeField] private GameObject BalanceBar;
    [SerializeField] private GameObject BalanceTriangle;
    [SerializeField] private Image HurtImageMask;
    [SerializeField] private Image TurnRightSignal;
    [SerializeField] private Image TurnLeftSignal;
    [SerializeField] private Text ObjectiveTitle;
    [SerializeField] private Text ObjectiveListTextBox;
    [SerializeField] private Text DeliveredTextBox;
    [SerializeField] private Text CountdownText;
    [SerializeField] private Text GameOverText;
    private bool HurtMaskisVisible;
    private bool DeliveredTextIsVisible;
    private bool TurnRightSignalIsVisible;
    private bool TurnLeftSignalIsVisible;

    void Start()
    {
        HurtMaskisVisible = false;
        TurnRightSignalIsVisible = false;
        TurnLeftSignalIsVisible = false;
        setVisibilityToBalanceBar(false);

        setImageAlpha(ref HurtImageMask, 0.0f);
        setImageAlpha(ref TurnRightSignal, 0.0f);
        setImageAlpha(ref TurnLeftSignal, 0.0f);
        DeliveredTextBox.color = new Color(DeliveredTextBox.color.r,
                    DeliveredTextBox.color.g,
                    DeliveredTextBox.color.b,
                    0.0f);
        GameOverElements.SetActive(false);
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

        if (DeliveredTextIsVisible)
        {
            float alpha = DeliveredTextBox.color.a - 0.01f;

            if (alpha > 0)
            {
                DeliveredTextBox.color = new Color(DeliveredTextBox.color.r, 
                    DeliveredTextBox.color.g, 
                    DeliveredTextBox.color.b, 
                    alpha);
            }
            else
            {
                DeliveredTextBox.color = new Color(DeliveredTextBox.color.r,
                    DeliveredTextBox.color.g,
                    DeliveredTextBox.color.b,
                    0.0f);
                DeliveredTextIsVisible = false;
            }
        }
    }

    public void setCurrentLives(int currentLives, int maxLives)
    {
        LifeText.text = (currentLives > 0) ? "Lives " + currentLives + "/" + maxLives :
                                             "Lives " + 0 + "/" + maxLives;
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

    public void setObjectiveListText(Objective currentObjective, List<Objective> objList)
    {
        setObjectiveListText("");
        for (int i = 0; i < objList.Count; i++)
        {
            if (currentObjective.getObjectiveID() == objList[i].getObjectiveID())
            {
                setObjectiveListText(" * " + currentObjective.getObjectiveName());
            }
        }

        for (int i = 0; i < objList.Count; i++)
        {
            if (currentObjective.getObjectiveID() != objList[i].getObjectiveID())
            {
                setObjectiveListText(ObjectiveListTextBox.text + "\n" + objList[i].getObjectiveName());
            }
        }
    }

    public void setObjectiveListText(List<Objective> objList)
    {
        setObjectiveListText("");
        for (int i = 0; i < objList.Count; i++)
        {
            string text = (i > 0) ? "\n" + objList[i].getObjectiveName() :
                                    objList[i].getObjectiveName();
            setObjectiveListText(this.ObjectiveListTextBox.text + text);
        }
    }
    
    public void setObjectiveListText(string text)
    {
        ObjectiveListTextBox.text = text;
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

    public void setVisibilityToObjectives(bool visible)
    {
        ObjectiveListTextBox.color = (visible) ? new Color(1.0f, 1.0f, 1.0f, 1.0f) :
            new Color(0.0f, 0.0f, 0.0f, 0.0f);
        ObjectiveTitle.color = (visible) ? new Color(1.0f, 1.0f, 1.0f, 1.0f) :
            new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    public void setVisiblilityToDeliveredText(bool val)
    {
        DeliveredTextBox.color = new Color(DeliveredTextBox.color.r, DeliveredTextBox.color.g, DeliveredTextBox.color.b, 1.0f);
        DeliveredTextIsVisible = val;
    }

    public void setVisibilityToGameOverText(bool val)
    {
    }

    public void setVisibilityToBalanceBar(bool val)
    {
        BalanceTriangle.SetActive(val);
        BalanceBar.SetActive(val);
    }

    public void setDeliveredText(string text)
    {
        DeliveredTextBox.text = "Delivered\n" + text;
    }

    public void goToGameOverScreen()
    {
        HUDElements.SetActive(false);
        GameOverElements.SetActive(true);
        GameOverText.text = "GAME OVER";
    }

    public void goToNextLevelScreen()
    {
        HUDElements.SetActive(false);
        GameOverElements.SetActive(true);
        GameOverText.text = "SUCCESS!";
    }

    public void setCountDownText(string text)
    {
        CountdownText.text = text;
    }
}
