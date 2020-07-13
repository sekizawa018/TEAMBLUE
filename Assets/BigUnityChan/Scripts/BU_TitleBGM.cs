using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_TitleBGM : MonoBehaviour
{
    AudioSource bgm;

    void Start()
    {
        bgm = GetComponent<AudioSource>();
        bgm.time = 9.1f;
        bgm.Play();
    }

}
