using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieAI_ys : MonoBehaviour
{
    //全てのステージセル
    StageCell_ys[] stageCells;
    public GameObject cells { get; set; }


    //ユニティちゃん
    public GameObject unityChan { get; set; }

    //移動したかどうか
    bool isMove;

    //各方向移動制限
    const float limitMaxX = 2;
    const float limitMinX = -2;
    const float limitMaxZ = 8;
    const float limitMinZ = -2;

    Animator zombieAnimator;

    public Canvas canvas;

    StageCell_ys targetSc;
    StageCell_ys originalSc;

    public int zombieHP { get; set; }



    // Start is called before the first frame update
    void Start()
    {
        zombieHP = Random.Range(5, 8);
        stageCells = cells.GetComponentsInChildren<StageCell_ys>();
        zombieAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        canvas.transform.rotation =
          Camera.main.transform.rotation;
    }


    //Unityちゃんが隣接しているか？
    bool IsAdjoiningUnityChan()
    {
      

        int unityZ = (int)unityChan.transform.localPosition.z;
        int unityX = (int)unityChan.transform.localPosition.x;
        int zombieZ = (int)transform.localPosition.z;
        int zombieX = (int)transform.localPosition.x;

        if ((Mathf.Abs(unityZ - zombieZ) == 2 && unityX == zombieX)||(Mathf.Abs(unityX-zombieX)==2&&unityZ==zombieZ))
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

        transform.rotation= Quaternion.Euler(new Vector3(0, 180, 0));

    }

    //移動処理
    void MoveCell()
    {
        if (isMove)
        {
            return;
        }
        else
        {
            int diffZ = (int)(unityChan.transform.localPosition.z - transform.localPosition.z);
            if (diffZ < 0)
            {
                MoveForward();
            }else if (diffZ > 0)
            {
                MoveBack();
            }
            if (!isMove)
            {
                int diffX = (int)(unityChan.transform.localPosition.x - transform.localPosition.x);
                if (diffX < 0)
                {
                    MoveRight();
                    MoveCell();
                }
                else
                {
                    MoveLeft();
                    MoveCell();
                }
            }
        }
    }

    //ターンが回ってきた時の処理
    public void Action()
    {
        if (IsAdjoiningUnityChan())
        {
            Attack();
        }
        else
        {
            MoveCell();
            isMove = false;
            if (IsAdjoiningUnityChan())
            {
                Attack();
            }
            else
            {
                return;
            }
        }
    }

    public void MoveForward()
    {
        Vector3 targetPos = transform.localPosition + Vector3.back * 2;
        if (targetPos.z <= limitMaxZ)
        {
            MoveCheck(targetPos);
        }
        else
        {
            return;
        }

    }
    public void MoveBack()
    {
        Vector3 targetPos = transform.localPosition + Vector3.forward * 2;
        if (targetPos.z >= limitMinZ)
        {
            MoveCheck(targetPos);
        }
        else
        {
            return;
        }
    }
    public void MoveRight()
    {
        Vector3 targetPos = transform.localPosition + Vector3.left * 2;
        if (targetPos.x <= limitMaxX)
        {
            MoveCheck(targetPos);
        }
        else
        {
            return;
        }
    }
    public void MoveLeft()
    {
        Vector3 targetPos = transform.localPosition + Vector3.right * 2;
        if (targetPos.x >= limitMinX)
        {
            MoveCheck(targetPos);
        }
        else
        {
            return;
        }
    }

    void MoveCheck(Vector3 targetPos)
    {
      
        foreach (var cell in stageCells)
        {
            if (cell.posZ == (int)targetPos.z && cell.posX == (int)targetPos.x)
            {
                targetSc=cell;
            }
            if (cell.posZ == (int)transform.localPosition.z && cell.posX == (int)transform.localPosition.x)
            {
                originalSc = cell;
            }
        }
        if (targetSc.isMount == false)
        {
            transform.position = targetPos;
            targetSc.isMount = true;
            originalSc.isMount = false;
            isMove = true;
          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Bullet"))
        {
            zombieAnimator.SetTrigger("Damage");
        }
    }
}
