using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameOverScreen : MonoBehaviour {

    [SerializeField] private Text gameOverText;
    [SerializeField] private Text finalScoreLabel;
    [SerializeField] private Text TimeCompletedLabel;
    [SerializeField] private Text TimeCompletedText;

    public void setScore(string text)
    {
        this.finalScoreLabel.text = text;
    }

    public void setTimeText(float timeInSeconds)
    {
        TimeSpan t = TimeSpan.FromSeconds(timeInSeconds);
        string formattedTime = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", t.Hours, t.Minutes, t.Seconds);
        TimeCompletedText.text = formattedTime;
    }
}
