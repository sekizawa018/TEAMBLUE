using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCon_ys : MonoBehaviour
{
    public GameObject Bullet;

    public string ShootType;

    public GameObject unityChanObj;
    Transform unityChan;

    GameObject Bullets;

    public int WeaPonPower;

    AudioSource AS;

    public int MaxAm; 
    public int remainingAm { get; set; }

    public int energieCost;


    // Start is called before the first frame update
    void Start()
    {
        Bullets = GameObject.Find("Mag/Bullets").gameObject;
        AS = GetComponent<AudioSource>();
        remainingAm = MaxAm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateBullet()
    {
        unityChan = unityChanObj.GetComponent<Transform>();

        AS.Play();
        GameObject Bu = Instantiate(Bullet,
            Bullets.transform.position,
            Bullet.transform.rotation*unityChan.transform.rotation);




        Bu.GetComponent<BulletCon_ys>().BulletDamage = Random.Range(WeaPonPower - 2, WeaPonPower + 1);
        Bu.GetComponent<Rigidbody>().velocity = unityChan.transform.forward * 10f;
    }

    public void SgInstantiateBullet()
    {
        AS.Play();
        unityChan = unityChanObj.GetComponent<Transform>();
        for (int i = -2; i <= 2; i++)
        {
            GameObject Bu = Instantiate(Bullet,
                Bullets.transform.position,
                Bullet.transform.rotation * unityChan.transform.rotation);
            Bu.GetComponent<BulletCon_ys>().BulletDamage = Random.Range(WeaPonPower - 2, WeaPonPower + 1);
            Bu.GetComponent<Rigidbody>().velocity = (unityChan.transform.forward * 10f)+new Vector3(i, 0, 0);
        }
    }

    public string getShootType()
    {
        return ShootType;
    }
}
