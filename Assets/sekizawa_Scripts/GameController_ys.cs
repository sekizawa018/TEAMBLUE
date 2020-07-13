using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController_ys : MonoBehaviour
{
    enum State
    {
        Ready,
        ObjectSet,
        PlayerTurn,
        TargetSelect,
        Move,
        EnemyTurn,
        GameOver
    }

    State state;

    int turnCount;

    public GameObject unitychanObj;
    UnityChanCon2_ys unitychan;
    public GameObject[] zombiePrefabs;
    List<GameObject> zombieList = new List<GameObject>();

    public GameObject cells;
    public GameObject TargetMarker;

    public GameObject TrashCan;
    public List<GameObject> TrashCanList = new List<GameObject>();

    public GameObject ShootButton;
    public GameObject MoveButton;
    public GameObject MoveEndButton;
    public GameObject TurnEndButton;
    public LifeAndEnergiePanel_ys lifeAndEnergie;

    public GameObject Reload;

    public Text ARam;
    public Text Pam;
    public Text SGam;

    public Text nowState;
    public GameObject WeaponsButton;

    public GameObject moveCell;
    GameObject moce;

    public GameObject TargetZombie { get; set; }
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        TargetMarker.SetActive(false);
        unitychan = unitychanObj.GetComponent<UnityChanCon2_ys>();
        Ready();
        ShootButton.SetActive(false);
        MoveButton.SetActive(false);
        MoveEndButton.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
       
        if (TargetZombie != null)
        {
            ShootButton.SetActive(true);
        }
        else
        {
            ShootButton.SetActive(false);
        }
       

        foreach(var zombie in zombieList)
        {
            ZombieAI2_ys zo = zombie.GetComponent<ZombieAI2_ys>();
            if (zo.zombieHP <= 0)
            {
                zo.zombieAnimator.SetBool("IsDie", true);
                zombieList.Remove(zombie);
                TargetMarker.SetActive(false);

            }
        }

        ARam.text = "" + unitychan.Weapons[0].GetComponent<WeaponCon_ys>().remainingAm + "/" + unitychan.Weapons[0].GetComponent<WeaponCon_ys>().MaxAm;
        Pam.text = "" + unitychan.Weapons[1].GetComponent<WeaponCon_ys>().remainingAm + "/" + unitychan.Weapons[1].GetComponent<WeaponCon_ys>().MaxAm;
        SGam.text = "" + unitychan.Weapons[2].GetComponent<WeaponCon_ys>().remainingAm + "/" + unitychan.Weapons[2].GetComponent<WeaponCon_ys>().MaxAm;
        lifeAndEnergie.UpdateEnergie(unitychan.energy);
        lifeAndEnergie.UpdateLife(unitychan.unityChanHP);

        if (unitychan.unityChanHP <= 0)
        {
            GameOver();
        }
       
        switch (state)
        {
            case State.Ready:
                ObjectSet();
                break;
            case State.ObjectSet:
                unitychan.energy = 4;
            
                PlayerTurn();
               
                break;
            case State.PlayerTurn:       
                if (unitychan.energy <= 0)
                {
                    Reload.SetActive(false);
                    WeaponsButton.SetActive(false);
                    ShootButton.SetActive(false);
                    MoveButton.SetActive(false);
                    MoveEndButton.SetActive(false);
                }
                break;
            case State.Move:
                unitychan.MoveUnityChan(startPos);
                Reload.SetActive(false);
                MoveButton.SetActive(false);
                MoveEndButton.SetActive(true);
                WeaponsButton.SetActive(false);
                moce.GetComponent<Renderer>().material.color = new Color(0, 0.1098f, 1, Mathf.Clamp(Mathf.Sin(Time.time), 0.1f, 0.6f));

                break;
            case State.TargetSelect:
             
                RayShoot();
                break;

            case State.EnemyTurn:
                bool isend = true;

                foreach (var zombie in zombieList)
                {

                    if (zombie.GetComponent<ZombieAI2_ys>().isActionEnd == false)
                    {
                        isend = false;
                    }

                }
                if (isend)
                {
                    ObjectSet();
                }
                break;
            case State.GameOver:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("Sekizawa_Main");
                }

                break;
        }
    }

    void Ready()
    {
        state = State.Ready;
        nowState.text = "Ready";

        CreatZombie();

       
    }

    void ObjectSet()
    {
        state = State.ObjectSet;
        nowState.text = "ObjectSet";
        turnCount += 1;


        int rand = Random.Range(1, turnCount/2);

        for (int i = 0; i <= rand; i++)
        {
            if (zombieList.Count == 0 || turnCount % 2 == 0)
            {
                CreatZombie();
            }
        }

        if (turnCount % 3 == 0)
        {
            CreatTrashCan();
        }
           
      
    }

   public  void PlayerTurn()
    {
        state = State.PlayerTurn;
        nowState.text = "PLAYER TURN"+turnCount;
        MoveButton.SetActive(true);
        WeaponsButton.SetActive(true);
        TurnEndButton.SetActive(true);
        Reload.SetActive(true);
       
    }

    public void TargetSelect()
    {
        state = State.TargetSelect;
        nowState.text = "TARGET SELECT";
        MoveButton.SetActive(false);
    }

    public void Move()
    {
        state = State.Move;
        nowState.text = "MOVE";
        startPos = unitychanObj.transform.localPosition;
        moce = Instantiate(moveCell,
            startPos,
            Quaternion.identity);
    }


    public void EnemyTurn()
    {
        state = State.EnemyTurn;
        nowState.text = "ENEMY TURN";

        Destroy(moce);
        TurnEndButton.SetActive(false);
        Reload.SetActive(false);
        TargetMarker.SetActive(false);
        MoveButton.SetActive(false);
        MoveEndButton.SetActive(false);
        TargetZombie = null;

     
        foreach (var zombie in zombieList)
        {
            zombie.GetComponent<ZombieAI2_ys>().Action();
        }

       
        

    }
    void GameOver()
    {
        state = State.GameOver;
        nowState.text = "Gameover";
        unitychan.Die();
        
    }


   


    void CreatZombie()
    {
        float x = Random.Range(-1, 9);
        float z = Random.Range(-1, 9);

       






       // float x = Random.Range(-1.5f, 1.5f);
       // float z = Random.Range(6.0f, 8.0f);

        GameObject zo = Instantiate(zombiePrefabs[0],
          new Vector3(x, 3f, z),
          zombiePrefabs[0].transform.rotation);

        ZombieAI2_ys zoComponent = zo.GetComponent<ZombieAI2_ys>();

        zoComponent.unityChan = this.unitychanObj;
        zoComponent.zombieHP = Random.Range(5, 8);


        zombieList.Add(zo);
    }

    void CreatTrashCan()
    {

        float x = Random.Range(-2, 8);
        float z = Random.Range(-2, 8);


        //float x = Random.Range(-1.5f, 1.5f);
        //float z = Random.Range(-1.0f, 4.0f);

        GameObject tr = Instantiate(TrashCan,
          new Vector3(x, 3f, z),
          TrashCan.transform.rotation);

        tr.GetComponent<ObjCon_ys>().gm = GetComponent<GameController_ys>();

        TrashCanList.Add(tr);
    }

    void RayShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
          

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Zombie")
                {
                  
                    TargetZombie = hit.collider.gameObject;
                    TargetMarker.SetActive(true);
                    TargetMarker.transform.position = hit.collider.gameObject.transform.localPosition+Vector3.up+(Vector3.back/2);
                }
            }
        }
    }

    public void OnMoveEndButton()
    {
        MoveEndButton.SetActive(false);
        WeaponsButton.SetActive(true);
        unitychan.SpeedStateReset();
        unitychan.energy -= 1;
        PlayerTurn();
        Destroy(moce);
    }

    public void OnBacktoTitle()
    {
        SceneManager.LoadScene("Sekizawa_Title");
    }

}
