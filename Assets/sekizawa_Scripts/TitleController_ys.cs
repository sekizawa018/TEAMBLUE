using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController_ys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartBuuton()
    {
        SceneManager.LoadScene("Sekizawa_Main");
    }
    public void OnBackTitle()
    {
        SceneManager.LoadScene("TeamBLUE_MainTitle");
    }
}
