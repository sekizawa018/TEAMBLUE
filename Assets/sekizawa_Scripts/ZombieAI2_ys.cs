using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieAI2_ys : MonoBehaviour
{
   


    //ユニティちゃん
    public GameObject unityChan { get; set; }


    //各方向移動制限
    const float limitMaxX = 6.5f;
    const float limitMinX = -0.5f;
    const float limitMaxZ = 6.5f;
    const float limitMinZ = -0.5f;

    public Animator zombieAnimator;

    public Canvas canvas;

    bool moveTrigger;

    Vector3 player_pos;

    public bool isActionEnd { get; set; }
  

    public int zombieHP { get; set; }

    AudioSource AS;

    public AudioClip[] ACList;



    // Start is called before the first frame update
    void Start()
    {
        zombieHP = Random.Range(5, 8);
     
        zombieAnimator = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        canvas.transform.rotation =
          Camera.main.transform.rotation;

        player_pos = transform.position; //プレイヤーの位置を取得

        player_pos.x = Mathf.Clamp(player_pos.x, limitMinX, limitMaxX); //x位置が常に範囲内か監視
        player_pos.z = Mathf.Clamp(player_pos.z, limitMinZ, limitMaxZ);
        transform.position = new Vector3(player_pos.x, player_pos.y, player_pos.z); //範囲内であれば常にその位置がそのまま入る

        zombieAnimator.SetFloat("Speed", transform.GetComponent<Rigidbody>().velocity.magnitude*100000);

        if (moveTrigger)
        {
            transform.LookAt(unityChan.transform);
            Vector3 pos = transform.localPosition + transform.forward * 1.3f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, pos, Time.deltaTime);
           
           
            
                
        }
    }


    //Unityちゃんが隣接しているか？
    bool IsAdjoiningUnityChan()
    {


        float unityZ = unityChan.transform.localPosition.z;
        float unityX = unityChan.transform.localPosition.x;
        float zombieZ = transform.localPosition.z;
        float zombieX = transform.localPosition.x;

        if (Mathf.Abs(unityZ - zombieZ) < 1 && Mathf.Abs(unityX - zombieX) <1)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    //攻撃
    void Attack()
    {
        //Unityちゃんの方向を向く
        transform.LookAt(unityChan.transform);


        //攻撃処理
        Debug.Log("Attack");

        zombieAnimator.SetTrigger("Attack");

        AS.clip = ACList[0];
        AS.Play();
        unityChan.GetComponent<UnityChanCon2_ys>().Damage();

        StartCoroutine("StopCoroutine");



    }

    IEnumerator StopCoroutine()
    {
        yield return new WaitForSeconds(1f);
        isActionEnd = true;
    }

    IEnumerator MoveAndAttack()
    {
        yield return new WaitForSeconds(1f);
        Attack();
    }

    IEnumerator MoveStop()
    {
        yield return new WaitForSeconds(1f);
        moveTrigger = false;
        if (IsAdjoiningUnityChan())
        {
            StartCoroutine("MoveAndAttack");
        }
        else
        {
            StartCoroutine("StopCoroutine");
        }
    }

    void MoveCell()
    {
     

        moveTrigger = true;
        AS.clip = ACList[1];
        AS.Play();
        StartCoroutine("MoveStop");

        
      
    }

    //ターンが回ってきた時の処理
    public void Action()
    {
        isActionEnd = false;
        if (IsAdjoiningUnityChan())
        {
            Attack();
        }
        else
        {
            MoveCell();
        }
    }

    

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Bullet"))
        {
            zombieAnimator.SetTrigger("Damage");
            AS.clip = ACList[2];
            AS.Play();
           
            canvas.GetComponent<EnemyHpCanvasCon_ys>().DiminishHpPanel(other.transform.parent.GetComponent<BulletCon_ys>().BulletDamage);

            zombieHP -= other.transform.parent.GetComponent<BulletCon_ys>().BulletDamage;

            if (!(other.tag == "AR_Bullet"))
            {
                Destroy(other.gameObject);
            }
        }
    }

    void DieTiming()
    {
       
        Destroy(gameObject);
    }
}
