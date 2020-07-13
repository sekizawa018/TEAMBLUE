using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Sensor : MonoBehaviour
{
    public BU_CharaMove cm;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Ground" || transform.position.y<0.3f || other.gameObject.tag=="Ignore")
        {
            cm.IsJumping(false);
        }
    }
}
