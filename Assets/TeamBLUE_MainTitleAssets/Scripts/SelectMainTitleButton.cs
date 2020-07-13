using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

//各ゲームシーンへ遷移するためのスクリプト

public class SelectMainTitleButton : MonoBehaviour
{
	string[] selectGameTitle = { //遷移するシーン名を初期値として配列に
        "", //番号を合わせるための空要素
        "Sekizawa_Title", //関澤
        "BigUnityChanTitle", //藤峰
        "TestTitle03", //唐澤
        "GiftyChan_Title"  //土屋
    };

	static int[] selectGameFlag = { 0, 0, 0, 0, 0 }; //クリア判定のための配列(最初の要素は番号を合わせるための空要素)

    void Start(){
		Debug.Log("Start");
		Debug.Log(selectGameFlag.Sum());
		if(selectGameFlag.Sum()>=4){ //それぞれのゲーム全てからタイトルに戻ってきたら
			Debug.Log("ALL GamePlaying!!!");
			for (int i = 0; i < selectGameFlag.Length;i++){ //初期値を0にリセット
				selectGameFlag[i] = 0;
			}
				SceneManager.LoadScene("AllPlaying"); //クリアシーンへ遷移
		}
	}
	//Buttonにnumberを割り振ってnumberを元にselectGameTitleのインデックスとして各ゲームシーンに遷移
	public void OnClick(int number){
		Debug.Log(number);
		if(selectGameTitle[number]!=""){
			selectGameFlag[number] = 1;
			SceneManager.LoadScene(selectGameTitle[number]);
        }
	}

	
}
