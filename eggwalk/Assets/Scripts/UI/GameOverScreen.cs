using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameOverScreen : MonoBehaviour {

    [SerializeField] private Text gameOverText;
    [SerializeField] private Text finalScoreLabel;
    [SerializeField] private Text TimeCompletedLabel;
    [SerializeField] private Text TimeCompletedText;
	[SerializeField] private Image BackgroundImage;

	[SerializeField] private Image RestartButtonImage;
	[SerializeField] private Image ControlsButtonImage;
	[SerializeField] private Image MenuButtonImage;

	[SerializeField] private Text RestartButtonText;
	[SerializeField] private Text ControlsButtonText;
	[SerializeField] private Text MenuButtonText;


	void Start()
	{
		StartCoroutine(FadeImageToFullAlpha (12.5f, BackgroundImage, 0.5f));

		StartCoroutine(FadeTextToFullAlpha (1.75f, gameOverText, 1.0f));

		StartCoroutine(FadeTextToFullAlpha (1.75f, finalScoreLabel, 1.075f));

		StartCoroutine(FadeImageToFullAlpha (1.0f, RestartButtonImage, 3.175f));
		StartCoroutine(FadeTextToFullAlpha (1.0f, RestartButtonText, 3.25f));

		StartCoroutine(FadeImageToFullAlpha (1.0f, ControlsButtonImage, 3.25f));
		StartCoroutine(FadeTextToFullAlpha (1.0f, ControlsButtonText, 3.325f));

		StartCoroutine(FadeImageToFullAlpha (1.0f, MenuButtonImage, 3.325f));
		StartCoroutine(FadeTextToFullAlpha (1.0f, MenuButtonText, 3.4f));

		//StartCoroutine(FadeTextToFullAlpha (1.75f, TimeCompletedLabel, 1.075f));
		//StartCoroutine(FadeTextToFullAlpha (1.75f, TimeCompletedText, 1.075f));

	}
		

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
		

	public IEnumerator FadeImageToFullAlpha(float time, Image image, float waitTime)
	{
		float target = image.color.a;
		image.color = new Color (image.color.r, image.color.g, image.color.b, 0);

		yield return new WaitForSeconds (waitTime);

		while (image.color.a < target) {
			image.color = new Color (image.color.r, image.color.g, image.color.b, image.color.a + (Time.deltaTime / time));
			yield return null;
		}
	}

	public IEnumerator FadeTextToFullAlpha(float time, Text text, float waitTime)
	{
		float target = text.color.a;
		text.color = new Color (text.color.r, text.color.g, text.color.b, 0);

		yield return new WaitForSeconds (waitTime);

		while (text.color.a < target) {
			text.color = new Color (text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / time));
			yield return null;
		}
	}

}
