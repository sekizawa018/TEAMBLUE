using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_ObjController : MonoBehaviour
{
    public ParticleSystem puff;
    AudioSource sound;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.tag == ("Foot"))
        {
            GameObject pare = gameObject.transform.parent.gameObject;
            sound = pare.GetComponent<AudioSource>();
            
            sound.Play();
            Debug.Log(sound.isPlaying);

            Destroy(pare, 0.1f);
            ParticleSystem effect = Instantiate(
                        puff,
                        transform.position,
                        Quaternion.identity
                         );
            Destroy(effect, 3.0f);

            GameObject player = other.transform.root.gameObject;
            BU_CharaMove cm = player.GetComponent<BU_CharaMove>();
            Vector3 scale = cm.GetScale();
            scale.x += 0.4f;
            scale.y += 0.4f;
            scale.z += 0.4f;
            cm.SetScale(scale);

            cm.SetMass(cm.GetMass() + scale.y * 0.8f);
            cm.SetScore(cm.GetScore() + 1);
        }
    }
}

