using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
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
		Vector3 unityChanPos = unityChan.transform.position;

		//カメラとプレイヤーの位置を同じにする
		transform.position = new Vector3(unityChanPos.x, -0.14f, -40);
	}
}
