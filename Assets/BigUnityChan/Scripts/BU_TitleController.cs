using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BU_TitleController : MonoBehaviour
{
    public Text highScore;

    public void Start()
    {
        if (PlayerPrefs.GetInt("HighScore") == 0)
        {
            highScore.text = "<↑ Go> <← → Turn> <SpaceKey Jump>";
        }
        else
        {
            highScore.text = $"HighScore {PlayerPrefs.GetInt("HighScore")}";
        }
    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("BigUnityChanGame");
    }

    public void OnBackMenuButtonClicked()
    {
        SceneManager.LoadScene("TeamBLUE_MainTitle");
    }
}
