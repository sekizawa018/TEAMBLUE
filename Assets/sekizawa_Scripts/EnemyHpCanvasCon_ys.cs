using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpCanvasCon_ys : MonoBehaviour
{
    public float offset;
    public GameObject redPanel;
    public GameObject zombie;
    GameObject backPanel;
    int zombieHP;
    float redPanelSize;
    List<GameObject> panelList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        backPanel = transform.GetChild(0).gameObject;
        zombieHP = zombie.GetComponent<ZombieAI2_ys>().zombieHP;

        float usable = backPanel.GetComponent<RectTransform>().sizeDelta.x - (zombieHP+1) * offset;
        redPanelSize = usable / zombieHP;

      

        for (int i = 0; i < zombieHP; i++)
        {
           GameObject red= Instantiate(redPanel,
                new Vector3((redPanelSize / 2+(redPanelSize*i)) + offset*(i+1), 0, 0),
                Quaternion.identity);
           
            red.GetComponent<RectTransform>().sizeDelta = new Vector2(redPanelSize, 100);
           
            red.transform.SetParent(backPanel.transform,false);

            panelList.Add(red);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DiminishHpPanel(int x)
    {
        zombieHP = zombie.GetComponent<ZombieAI2_ys>().zombieHP;
        for (int i = 0; i < x; i++)
        {
            Destroy(panelList[zombieHP-1-i]);
            panelList.RemoveAt(zombieHP-1-i);
            if (panelList.Count == 0)
            {
                return;
            }
        }
       
    }
}
