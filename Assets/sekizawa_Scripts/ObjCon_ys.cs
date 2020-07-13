using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCon_ys : MonoBehaviour
{

    Vector3 player_pos;
    const float limitMaxX = 2;
    const float limitMinX = -2;
    const float limitMaxZ = 8;
    const float limitMinZ = -2;

    public GameController_ys gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player_pos = transform.position; //プレイヤーの位置を取得

        player_pos.x = Mathf.Clamp(player_pos.x, limitMinX, limitMaxX); //x位置が常に範囲内か監視
        player_pos.z = Mathf.Clamp(player_pos.z, limitMinZ, limitMaxZ);
        transform.position = new Vector3(player_pos.x, player_pos.y, player_pos.z); //範囲内であれば常にその位置がそのまま入る

    }
    private void OnTriggerEnter(Collider other)
    {
     
        if (other.tag.Contains("Bullet"))
        {
            Destroy(gameObject);

            if (!(other.tag == "AR_Bullet"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
