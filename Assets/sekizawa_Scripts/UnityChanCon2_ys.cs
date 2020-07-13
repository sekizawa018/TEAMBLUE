using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnityChanCon2_ys : MonoBehaviour
{
    

    //各方向移動制限
    const float limitMaxX = 8.5f;
    const float limitMinX = -2.5f;
    const float limitMaxZ = 8.5f;
    const float limitMinZ = -2.5f;

    public int unityChanHP { get; set; }

    public int energy;

    public GameObject[] Weapons;

    WeaponCon_ys activeWeapon;

   
    public GameController_ys GM;

    Vector3 player_pos;



    public float animSpeed = 1.5f;              // アニメーション再生速度設定
   
    public bool useCurves = true;               // Mecanimでカーブ調整を使うか設定する
                                                // このスイッチが入っていないとカーブは使われない
    public float useCurvesHeight = 0.5f;        // カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）

    // 以下キャラクターコントローラ用パラメタ
    // 前進速度
    public float forwardSpeed = 7.0f;
    // 後退速度
    public float backwardSpeed = 2.0f;
    // 旋回速度
    public float rotateSpeed = 2.0f;
   
    // キャラクターコントローラ（カプセルコライダ）の参照
    private CapsuleCollider col;
    private Rigidbody rb;
    // キャラクターコントローラ（カプセルコライダ）の移動量
    private Vector3 velocity;
    // CapsuleColliderで設定されているコライダのHeiht、Centerの初期値を収める変数
    private float orgColHight;
    private Vector3 orgVectColCenter;
    private Animator anim;                          // キャラにアタッチされるアニメーターへの参照
    private AnimatorStateInfo currentBaseState;         // base layerで使われる、アニメーターの現在の状態の参照

   
    // アニメーター各ステートへの参照
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int locoState = Animator.StringToHash("Base Layer.Locomotion");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    static int restState = Animator.StringToHash("Base Layer.Rest");



    // Start is called before the first frame update
    void Start()
    {
      

        

        // Animatorコンポーネントを取得する
        anim = GetComponent<Animator>();
        // CapsuleColliderコンポーネントを取得する（カプセル型コリジョン）
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
       
        // CapsuleColliderコンポーネントのHeight、Centerの初期値を保存する
        orgColHight = col.height;
        orgVectColCenter = col.center;

        unityChanHP = 3;
        energy = 4;
    }

    // Update is called once per frame
    void Update()
    {
        player_pos = transform.position; //プレイヤーの位置を取得

        player_pos.x = Mathf.Clamp(player_pos.x, limitMinX, limitMaxX); //x位置が常に範囲内か監視
        player_pos.z = Mathf.Clamp(player_pos.z, limitMinZ, limitMaxZ);
        transform.position = new Vector3(player_pos.x, player_pos.y,player_pos.z); //範囲内であれば常にその位置がそのまま入る
    }

    public void MoveUnityChan(Vector3 startPos)
    {
        float TlimitMaxX = startPos.x + 2;
        float TlimitMinX = startPos.x - 2;
        float TlimitMaxZ = startPos.z + 2;
        float TlimitMinZ = startPos.z - 2;

        float h = Input.GetAxis("Horizontal");              // 入力デバイスの水平軸をhで定義
        float v = Input.GetAxis("Vertical");                // 入力デバイスの垂直軸をvで定義
        anim.SetFloat("Speed", v);                          // Animator側で設定している"Speed"パラメタにvを渡す
        anim.SetFloat("Direction", h);                      // Animator側で設定している"Direction"パラメタにhを渡す
        anim.speed = animSpeed;                             // Animatorのモーション再生速度に animSpeedを設定する
      
       



        // 以下、キャラクターの移動処理
        velocity = new Vector3(0, 0, v);        // 上下のキー入力からZ軸方向の移動量を取得
                                                // キャラクターのローカル空間での方向に変換
        velocity = transform.TransformDirection(velocity);
        //以下のvの閾値は、Mecanim側のトランジションと一緒に調整する
        if (v > 0.1)
        {
            velocity *= forwardSpeed;       // 移動速度を掛ける
        }
        else if (v < -0.1)
        {
            velocity *= backwardSpeed;  // 移動速度を掛ける
        }



       


        Vector3 movement = velocity * Time.deltaTime;
        Vector3 newPos = transform.position + movement;

       
        Vector3 offset = newPos - startPos;
        transform.position = startPos + Vector3.ClampMagnitude(offset, 2.5f);







        // 左右のキー入力でキャラクタをY軸で旋回させる
        transform.Rotate(0, h * rotateSpeed, 0);



    }






    public void WeaponShoot()
    {
        if (activeWeapon.remainingAm >= 1&&activeWeapon.energieCost<=energy)
        {
            Vector3 pos = transform.position;
            GameObject zombie = GM.TargetZombie;
            transform.LookAt(zombie.transform);
            anim.SetTrigger(activeWeapon.ShootType);

            transform.position = pos;
            Invoke("ShootEnd", 3f);
            activeWeapon.remainingAm -= 1;
        }
        else
        {
            return;
        }
        


    }

    public void ShootTiming()
    {
        if (activeWeapon.getShootType()== "SG_shoot")
        {
            activeWeapon.SgInstantiateBullet();
        }
        else
        {
            activeWeapon.InstantiateBullet();
        }
        
    }

    public void SelectWeapon1()
    {
        activeWeapon = Weapons[0].GetComponent<WeaponCon_ys>();
        GM.TargetSelect();
        anim.SetBool("AR_set", true);
        Weapons[0].SetActive(true);
        Weapons[1].SetActive(false);
        Weapons[2].SetActive(false);
    }
    public void SelectWeapon2()
    {
        activeWeapon = Weapons[1].GetComponent<WeaponCon_ys>();
        GM.TargetSelect();
        anim.SetBool("AR_set", false);
        Weapons[0].SetActive(false);
        Weapons[1].SetActive(true);
        Weapons[2].SetActive(false);
    }
    public void SelectWeapon3()
    {
        activeWeapon = Weapons[2].GetComponent<WeaponCon_ys>();
        GM.TargetSelect();
        anim.SetBool("AR_set", true);
        Weapons[0].SetActive(false);
        Weapons[1].SetActive(false);
        Weapons[2].SetActive(true);
    }
    public void ShootEnd()
    {
        energy -= activeWeapon.energieCost;
        GM.PlayerTurn();
    }

    public void Damage()
    {
        unityChanHP -= 1;
        anim.SetTrigger("Damage");
    }

    public void Die()
    {
        anim.SetBool("Down", true);
    }

    public void SpeedStateReset()
    {
        anim.SetFloat("Speed", 0);
    }

    public void Reload()
    {
        if (energy >= 1)
        {
            activeWeapon.remainingAm = activeWeapon.MaxAm;
            energy -= 1;
        }
        else
        {
            return;
        }
        
    }
}
