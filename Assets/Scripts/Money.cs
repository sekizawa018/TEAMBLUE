using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    /*
	GameObject money10000;
	GameObject money5000;
	GameObject money1000;
	GameObject money500;
	GameObject money100;
	GameObject money10;
    */
	public static int moneyCount;
	AudioSource audioSource;

	// Start is called before the first frame update
	void Start()
    {
    GameObject money10000 = GameObject.FindWithTag("money10000");
		GameObject money5000 = GameObject.FindWithTag("money5000");
		GameObject money1000 = GameObject.FindWithTag("money1000");
		GameObject money500 = GameObject.FindWithTag("money500");
		GameObject money100 = GameObject.FindWithTag("money100");
		GameObject money10 = GameObject.FindWithTag("money10");
		audioSource = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
		
	}

    /*
    void OnCollitionEnter2D(Collision2D col){
		//Debug.Log("OnTriggerEnter2D");
		CheckMoney(col.gameObject);
	}
    */

	void OnTriggerEnter2D(Collider2D other)
	{
		CheckMoney(other.gameObject.tag);
	}

    void CheckMoney(string moneyTag){
        Debug.Log("CheckMoney");
		moneyTag=transform.tag;
		int money=0;
		switch (moneyTag)
		{
			case "money10000":
				money = 10000;
				break;
			case "money5000":
				money = 5000;
				break;
			case "money1000":
				money = 1000;
				break;
			case "money500":
				money = 500;
				break;
			case "money100":
				money = 100;
				break;
			case "money10":
				money = 10;
				break;
		}
		moneyCount += money;
		AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
		Destroy(gameObject);
		Debug.Log(moneyCount + "円");
	}
}
