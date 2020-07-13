using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BU_GameController : MonoBehaviour
{
    enum State
    {
        Ready,
        Play,
        GameOver
    }

    State state;

    const float LIMIT = 60.0f;
    float count = LIMIT;
    public BU_CharaMove cm;

    Vector3 startScale;
    public Text timeLabel;
    public Text stateLabel;
    public Text resultLabel;
    bool isOver = false;
    public BU_CameraController rotCamera;


    void Start()
    {
        string scene = SceneManager.GetActiveScene().name;
        startScale = new Vector3(1.0f, 1.0f, 1.0f);
        Ready();
    }

    void LateUpdate()
    {
        if (isOver)
        {
            rotCamera.RotCamera();
            Invoke("ReturnToTitle", 5.3f);
            enabled = false;

        }

        if (state == State.Play)
        {
            count -= Time.deltaTime;
        }

        timeLabel.text = $"Time {count.ToString("000")}";


        switch (state)
        {
            case State.Ready:
                cm.SetSteerActive(false);

                if (Input.GetButtonDown("Submit"))
                {
                    GameStart();
                }
                break;

            case State.Play:
                if (count <= 0)
                {
                    GameOver();
                }
                break;

            case State.GameOver:
                {
                    isOver = true;
                    break;
                }
        }
    }

    void Ready()
    {
        state = State.Ready;

        timeLabel.text = "";
        stateLabel.text = "at the pressing of a Enter, GameStart!!";
        resultLabel.text = "";

    }
    void GameStart()
    {
        state = State.Play;
        cm.SetSteerActive(true);
        stateLabel.text = "";

    }

    void GameOver()
    {
        state = State.GameOver;
        cm.SetSteerActive(false);

        stateLabel.text = "Time is up!!";
        resultLabel.text = $"Unity-Chan Broke {cm.GetScore()} items!!";
        if (PlayerPrefs.GetInt("HighScore") < cm.GetScore())
        {
            PlayerPrefs.SetInt("HighScore", cm.GetScore());
        }

    }

    void ReturnToTitle()
    {
        SceneManager.LoadScene("BigUnityChanTitle");
    }

}

