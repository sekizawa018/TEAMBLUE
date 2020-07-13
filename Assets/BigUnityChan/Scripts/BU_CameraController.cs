using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_CameraController : MonoBehaviour
{
    public Transform player;
    public Transform neck;
    float dist;
    float sec;
    Vector3 cameraPos;
    bool isOver = false;

    void LateUpdate()
    {
        sec += Time.deltaTime;

        if (isOver)
        {
            dist -= Time.deltaTime * player.localScale.y * 0.3f;

            dist = Mathf.Max(1f, dist);
            cameraPos.x = player.position.x + Mathf.Cos(sec * 0.7f) * player.localScale.y;
            cameraPos.y = neck.position.y;
            cameraPos.z = player.position.z + Mathf.Sin(sec * 0.7f) * player.localScale.y;
            transform.position = cameraPos;
            transform.LookAt(neck);
        }
        else
        {
            transform.position = player.position + (-player.forward * (player.localScale.y * 0.7f) * 3.0f) + (Vector3.up * (player.localScale.y * 0.8f) * 1.0f);
            transform.LookAt(player.position + Vector3.up * (player.localScale.y * 0.7f));
        }
    }

    public void RotCamera()
    {
        Debug.Log("rotcamera");
        isOver = true;
        //dist = player.localScale.y * 1.1f;
        dist = player.position.z - transform.position.z;
    }


}
