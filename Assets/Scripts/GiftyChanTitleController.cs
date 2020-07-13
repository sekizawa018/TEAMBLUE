using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GiftyChanTitleController : MonoBehaviour
{
	public Text HiscoreTimeLabel;
	public Text HiscoreMoneyLabel;

	void Awake()
	{
		ShowHiscore();
	}

	public void OnStartButtonClicked()
	{
		SceneManager.LoadScene("Stage1-01");
	}

	void ShowHiscore()
	{
		int[] moneyHiscore = GiftyGameController.GetMoneyHiscore();
		TimeSpan[] timeHiscore = GiftyGameController.GetTimeHiscore();

		HiscoreTimeLabel.text = "";
		HiscoreMoneyLabel.text = "";

		for (int i = 0; i < 3; i++)
		{
			HiscoreTimeLabel.text += timeHiscore[i].Minutes.ToString("00") + ":" + timeHiscore[i].Seconds.ToString("00") + "." + timeHiscore[i].Milliseconds.ToString("000") + "\n";
			HiscoreMoneyLabel.text += string.Format("{0:#,0}円", moneyHiscore[i]) + "\n";

		}
	}
}
