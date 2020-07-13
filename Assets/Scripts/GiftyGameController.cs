using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GiftyGameController : MonoBehaviour
{
	PlayerRun2D Unitychan2DRun;
	PlayerJump2D Unitychan2DJump;
	public GameObject unitychan;
	public GameObject startUnitychan;
	public GameObject clearUnitychan;
	public Text moneyLabel;
	public Text timeLabel;
	public Text stateLabel;

	public DateTime startDateTime; //スタート時のDateTime構造体
	public TimeSpan totalTime; //時間経過のTimeSpan構造体

	public DateTime readyStartDateTime; //Ready用
	public TimeSpan readyTotalTime; //Ready用
	public static int oldCountDown; //CountDown用
	public static bool isClear; //ClearPointにきてるか判定するために
	public static TimeSpan[] scoreTimes = { new TimeSpan(0, 59, 59), new TimeSpan(0, 59, 59), new TimeSpan(0, 59, 59), new TimeSpan(0, 59, 59) }; //時間のハイスコア用
	public static int[] scoreMoneys = { 0, 0, 0, 0 }; //取得金額のハイスコア用

	//List ghost=new List<List<bool>>(); //ゴースト用のlist
	static int f; //フレームのカウント

	GameObject Voice;
	AudioSource startVoice;

	GameObject bgm;
	AudioSource gameBGM;

	enum State
	{
		Ready,
		Play,
		Clear
	}

	State state;

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log("Strat() --------------------------------");
	}

	void Awake()
	{
		Voice = GameObject.FindGameObjectWithTag("StartVoice");
		startVoice=Voice.GetComponent<AudioSource>();
		bgm = GameObject.FindGameObjectWithTag("BGM");
		gameBGM = bgm.GetComponent<AudioSource>();
		Debug.Log("Awake()");
		Ready();
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log("state:" + state);
		switch (state)
		{
			case State.Ready:
				//現在のDateTimeから初期のDateTimeを計算し経過した時間を計算する
				readyTotalTime = DateTime.Now - readyStartDateTime;
				int countDown = (3 - readyTotalTime.Seconds);
				if (countDown != oldCountDown)
				{
					oldCountDown = countDown;
					//TimeSpan構造体のプロパティで秒をテキストにいれる
					stateLabel.text = countDown.ToString("0");
				}
				if (countDown == 0)
				{
					stateLabel.text = "届けて!!\nUnityちゃん!!";
					Debug.Log(stateLabel.text);
					GameStart();
				}
				break;
			case State.Play:
				//現在のDateTimeから初期のDateTimeを計算し経過した時間を計算する
				totalTime = DateTime.Now - startDateTime;
				//TimeSpan構造体のプロパティで分と秒をテキストにいれる
				timeLabel.text = totalTime.Minutes.ToString("00") + ":" + totalTime.Seconds.ToString("00") + "." + totalTime.Milliseconds.ToString("000");
				if (isClear == true)
				{
					Clear();
				}
				break;
			case State.Clear:
				if (Input.GetButtonDown("Fire1"))
				{
					Reload();
				}
				else{
					Invoke("Reload", 5f);
				}
				break;
		}
		moneyLabel.text = string.Format("{0:#,0}円", Money.moneyCount);
		//GhostRecording();
	}

	void Ready()
	{
		Debug.Log("Ready()");
		state = State.Ready;
		isClear = false;
		startUnitychan.SetActive(true);
		unitychan.SetActive(false);
		clearUnitychan.SetActive(false);

		Money.moneyCount = 0;
		moneyLabel.text = string.Format("{0:#,0}円", 0);
		timeLabel.text = "00:00.000";
		readyStartDateTime = DateTime.Now;
		oldCountDown = 0;
	}

	void GameStart()
	{
		startVoice.Play();
		gameBGM.time = 10f;
		Debug.Log("GameStart()");
		state = State.Play;
		Destroy(startUnitychan);
		unitychan.SetActive(true);
		Invoke("ClearStateLabel", 0.8f);

		//現在のDateTime構造体を設定
		startDateTime = DateTime.Now;
	}

	void Clear()
	{
		state = State.Clear;

		HiscoreMoneySort();
		HiscoreTimeSort();

		unitychan.SetActive(false);
		clearUnitychan.SetActive(true);
		gameBGM.Stop();
		stateLabel.gameObject.SetActive(true);
		stateLabel.text = "お金を\n" + string.Format("{0:#,0}円", Money.moneyCount) + " 拾って\n交番に届けたよ!!!";


		//Time.timeScale = Mathf.Approximately(Time.timeScale, 0f) ? 1f : 0f;

		//scoreMoney = Money.moneyCount;
		//SaveScoreMoney();
	}

	void Reload()
	{
		SceneManager.LoadScene("GiftyChan_Title");
	}

	public static void IsClear()
	{
		isClear = true;
	}

	void ClearStateLabel()
	{
		stateLabel.gameObject.SetActive(false);
		stateLabel.text = "";
	}

	void HiscoreMoneySort()
	{
		scoreMoneys[3] = Money.moneyCount;
		Array.Sort(scoreMoneys);
		Array.Reverse(scoreMoneys);
	}

	void HiscoreTimeSort()
	{
		scoreTimes[3] = totalTime;
		Array.Sort(scoreTimes);
	}

	public static int[] GetMoneyHiscore()
	{
		int[] gmh = new int[3];
		for (int i = 0; i < 3; i++)
		{
			gmh[i] = scoreMoneys[i];
		}
		return gmh;
	}

	public static TimeSpan[] GetTimeHiscore()
	{
		TimeSpan[] gth = new TimeSpan[3];
		for (int i = 0; i < 3; i++)
		{
			gth[i] = scoreTimes[i];
		}
		return gth;
	}


	void GhostRecording()
	{
		bool[] addGhost = new bool[3];

		//入力を受け取ってデータ化
		if (Input.GetKeyDown(KeyCode.Space))
		{
			addGhost[0] = true;
		}
		else
		{
			addGhost[0] = false;
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			addGhost[1] = true;
		}
		else
		{
			addGhost[1] = false;
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			addGhost[2] = true;
		}
		else
		{
			addGhost[2] = false;
		}
		//ghost.Add(addGhost);
	}

}
