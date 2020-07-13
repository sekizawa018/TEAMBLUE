using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTitleBack : MonoBehaviour
{
	public void OnMainTitleBackButtonClicked() //クリックされたらメインタイトルへ遷移
	{
		SceneManager.LoadScene("TeamBLUE_MainTitle");
	}
}
