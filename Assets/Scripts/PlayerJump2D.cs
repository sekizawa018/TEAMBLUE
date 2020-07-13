using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーキャラクターをジャンプさせる
//PlayerWalk2Dクラスから接地判定を受け取り、多段ジャンプさせないよう制御する
//解説記事 http://negi-lab.blog.jp/PlayerWalkAndJump2D

public class PlayerJump2D : MonoBehaviour
{
	Animator animator;
	AudioSource audioSource = null;
	GameObject[] audioObjects;

	//値はインスペクターから変更可能
	[SerializeField] float jumpPower = 700f;

	private PlayerRun2D playerRun;
	private Rigidbody2D rb;
	bool isClear;

	// Start is called before the first frame update
	private void Start()
    {
		audioObjects = GameObject.FindGameObjectsWithTag("AudioObject");

		//このスクリプトと同じゲームオブジェクトにアタッチされている
		//PlayerWalkスクリプトのコンポーネントを取得する
		playerRun = GetComponent<PlayerRun2D>();
		rb = GetComponent<Rigidbody2D>();

		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();

		GameObject clearPoint = GameObject.FindWithTag("ClearPoint");

	}

    // Update is called once per frame
    private void Update()
    {
		var audioObject = audioObjects[Random.Range(0, audioObjects.Length)];
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)){
            //接地している時のみ、ジャンプできる(多段ジャンプをさせない)
            if(playerRun.isGrounded){
				rb.AddForce(Vector2.up * jumpPower);
				//Debug.Log("Jump");

				animator.SetInteger("move", 1);
				//audioSource.Play();
				audioObject.GetComponent<AudioSource>().Play();
			}
			else{
				animator.SetInteger("move", 0);
			}
        }
    }

	private void OnTriggerEnter2D(Collider2D collider2D)
	{ 
		if (collider2D.gameObject.tag == "ClearPoint")
		{
			isClear = true;
			GiftyGameController.IsClear();
		}
	}
}
