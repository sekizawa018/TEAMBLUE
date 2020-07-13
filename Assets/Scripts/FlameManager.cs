using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameManager : MonoBehaviour
{
	//Unityちゃんを格納する変数
	public GameObject unityChan;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//Unityちゃんの位置を取得
		Vector3 unityChanPos = unityChan.transform.position;

		//ビデオプイレイヤーフレームとプレイヤーの位置を同じにする
		transform.position = new Vector3(unityChanPos.x, -0.16f, -30);
	}
}
