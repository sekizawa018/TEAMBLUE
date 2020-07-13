using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCell_ys : MonoBehaviour
{
    //自分の座標
    public int posX { get; set; }
    public int posZ { get; set; }

    //何か乗っているか
    public bool isMount { get; set; }
    

    // Start is called before the first frame update
    void Start()
    {
        posX = (int)transform.localPosition.x;
        posZ = (int)transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {

    }

   

   
}
