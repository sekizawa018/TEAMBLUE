using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitychanCon_ys : MonoBehaviour
{
    //各方向移動制限
    const float limitMaxX=2;
    const float limitMinX = -2;
    const float limitMaxZ = 2;
    const float limitMinZ = -2;

    //全てのステージセル
    StageCell_ys[] stageCells;

    public int energy;

    public GameObject[] Weapons;

    WeaponCon_ys activeWeapon;

    Animator unityAnime;
    public GameController_ys GM;

    StageCell_ys targetSc;
    StageCell_ys originalSc;

    // Start is called before the first frame update
    void Start()
    {
        stageCells = FindObjectsOfType<StageCell_ys>();
      
        unityAnime = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
                   
    }



    public void MoveForward()
    {
        Vector3 targetPos= transform.position + Vector3.forward*2;
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
        Vector3 targetPos = transform.position + Vector3.back * 2;
        
       
       
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
        Vector3 targetPos = transform.position + Vector3.right * 2;
      
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
        Vector3 targetPos = transform.position + Vector3.left * 2;
       
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
            if (cell.posZ == targetPos.z && cell.posX == targetPos.x)
            {
                targetSc = cell;
            }
            if (cell.posZ == transform.localPosition.z && cell.posX == transform.localPosition.x)
            {
                originalSc = cell;
            }
        }
        Debug.Log(targetSc.isMount);
        if (targetSc.isMount == false)
        {
            transform.position = targetPos;
            targetSc.isMount = true;
            originalSc.isMount = false;

            energy -= 1;
        }

    }

    public void WeaponShoot()
    {
        Vector3 pos = transform.position;
        GameObject zombie = GM.TargetZombie;
        transform.LookAt(zombie.transform);
        unityAnime.SetTrigger(activeWeapon.ShootType);

        transform.position = pos;
        Invoke("ShootEnd", 3f);
       
        
    }

    public void ARShootTiming()
    {
        activeWeapon.InstantiateBullet();
    }

    public void SelectWeapon1()
    {
        activeWeapon = Weapons[0].GetComponent<WeaponCon_ys>();
        GM.TargetSelect();
    }
    public void SelectWeapon2()
    {
        activeWeapon = Weapons[1].GetComponent<WeaponCon_ys>();
        GM.TargetSelect();
    }
    public void ShootEnd()
    {
        energy -= 1;
        transform.rotation = Quaternion.Euler(Vector3.forward);
        
        GM.PlayerTurn();
    }
}
