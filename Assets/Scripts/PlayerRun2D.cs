using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーキャラクター(ユニティちゃん)を左右に移動させる。向きの反転、接地判定も行う。
//解説記事 http://negi-lab.blog.jp/PlayerWalkAndJump2D

public class PlayerRun2D : MonoBehaviour
{
	Animator animator;

	int move = 0;

	//値はインスペクターから変更可能
	[SerializeField] float moveSpeed = 1000f; //わからない不具合を減らすために他のクラスからの値の変更不可に

	public bool isGrounded;

	private Rigidbody2D rb;
	private Vector3 defalutScale;

	bool isClear;

	public static float input_v;

	// Start is called before the first frame update
	private void Start()
	{
		//ゲームプレイ中、頻繁に呼び出されるコンポーネントはStart内でキャッシュしておく
		//毎回GetComponetすると負荷が高くなるため
		rb = GetComponent<Rigidbody2D>();

		//開始時のローカルスケールの値を記憶しておく
		defalutScale = transform.localScale;

		animator = GetComponent<Animator>();

		GameObject clearPoint = GameObject.FindWithTag("ClearPoint");
	}

	// Update is called once per frame
	private void Update()
	{   //左右方向の入力を受け取る
		var input_h = Input.GetAxis("Horizontal");
		//Debug.Log("入力受け取り");
		input_v = input_h;

		Run(input_h);

		Turn(input_h);
	}

	//水平方向の移動
	private void Run(float inputValue)
	{
		//Debug.Log("Run");
		if (inputValue != 0)
		{
			//接地していない時は水平方向に移動する力を弱める
			var mult = isGrounded ? 1f : 0.3f;

			//Rigidbody2Dに力を加えることでプレイヤーキャラクターを移動させる
			rb.AddForce(Vector2.right * inputValue * moveSpeed * mult * Time.deltaTime);
			if (!Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.UpArrow))
			{
				move = 2;
				animator.SetInteger("move", move);
			}
		}
		else
		{
			animator.SetInteger("move", 0);
		}
	}

	//向きを変える
	private void Turn(float inputValue)
	{
		//Debug.Log("Turn");
		if (inputValue > 0)
		{
			transform.localScale = defalutScale;
		}
		else if (inputValue < 0)
		{
			//ローカルスケールのXの値を反転させることで、スプライトを反転させる
			transform.localScale = new Vector3(-defalutScale.x, defalutScale.y, defalutScale.z);
		}
	}

	//接地判定
	private void OnCollisionEnter2D(Collision2D collision)
	{ //接地した時
		//Debug.Log("接地");
		isGrounded = true;
	}
	private void OnCollisionExit2D(Collision2D collision)
	{ //離れた時
		//Debug.Log("離脱");
		isGrounded = false;
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
