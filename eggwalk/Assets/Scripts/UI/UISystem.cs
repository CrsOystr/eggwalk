using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class UISystem : MonoBehaviour
{
    [SerializeField] private GameObject HUDElements;
    [SerializeField] private GameObject PauseElements;
    [SerializeField] private GameOverScreen GameOverElements;
    [SerializeField] private Text LifeText;
    [SerializeField] private BalanceBar Bar;
    [SerializeField] private GameObject Warning;
    [SerializeField] private Image HurtImageMask;
    [SerializeField] private Image HurtDeathImageMask;
    [SerializeField] private Image TurnRightSignal;
    [SerializeField] private Image TurnLeftSignal;
    [SerializeField] private Text ObjectiveTitle;
    [SerializeField] private Text ObjectiveListTextBox;
    [SerializeField] private Text DeliveredTextBox;
    [SerializeField] private Text CountdownText;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text FinalScoreLabel;
    [SerializeField] private List<string> deliveredMessages;

    private bool HurtMaskisVisible;
    private bool DeliveredTextIsVisible;
    private bool TurnRightSignalIsVisible;
    private bool TurnLeftSignalIsVisible;
    private bool GagueActivated;

    void Start()
    {
        HurtMaskisVisible = false;
        TurnRightSignalIsVisible = false;
        TurnLeftSignalIsVisible = false;

        setImageAlpha(ref HurtImageMask, 0.0f);
        setImageAlpha(ref TurnRightSignal, 0.0f);
        setImageAlpha(ref TurnLeftSignal, 0.0f);
        DeliveredTextBox.color = new Color(DeliveredTextBox.color.r,
                    DeliveredTextBox.color.g,
                    DeliveredTextBox.color.b,
                    0.0f);
        GameOverElements.gameObject.SetActive(false);
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
        SceneManager.LoadScene(level);
    }

    public void setRotationToBalanceBar(float angle)
    {
        Bar.IndicatorParent.transform.localEulerAngles = Vector3.forward * angle;
        Bar.setIndicatorColorFromAngle(angle, 75.0f);

        if (Mathf.Abs(angle) > 55.0f)
        {
            Bar.flashGauge();
        } else
        {
            Bar.resetGague();
        }
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

    public void setVisibilityToBalanceBar(bool val)
    {
        //BalanceTriangle.SetActive(val);
        //BalanceBar.SetActive(val);
    }

    public void setDeliveredText(string text)
    {
        if (deliveredMessages != null)
        {
            if (deliveredMessages.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, deliveredMessages.Count);
                text = deliveredMessages[index].ToUpper();
            }
        }

        DeliveredTextBox.text = text;
    }

    public void setFinalScore(int score)
    {
        this.FinalScoreLabel.text = "SCORE: " + score;
    }

    public void goToGameOverScreen(int finalScore)
    {
        setFinalScore(finalScore);
        HUDElements.SetActive(false);
        GameOverElements.gameObject.SetActive(true);
    }

    public void goToPauseScreen()
    {
        if (!PauseElements.activeSelf)
        {
            HUDElements.SetActive(false);
            PauseElements.SetActive(true);
        }
        else
        {
            HUDElements.SetActive(true);
            PauseElements.SetActive(false);
        }
    }
    public void goToNextLevelScreen()
    {
        HUDElements.SetActive(false);
        GameOverElements.gameObject.SetActive(true);
        HurtDeathImageMask.enabled = false;
    }

    public void setCountDownText(string text)
    {
        CountdownText.text = text;
    }

    public void setTimeText(float timeInSeconds)
    {
        this.GameOverElements.setTimeText(timeInSeconds);
    }

	public void setScoreText(int score) {
		this.ScoreText.text = "" + score;
	}

    public void activateWarningLabel(bool status)
    {
        this.Warning.GetComponent<WarningLabel>().Active = status;
        this.Warning.SetActive(status);
    }
}
