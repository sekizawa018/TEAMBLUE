using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_CharaMove : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    AudioSource aSource;
    private Vector3 scale;
    private float mass;
    public float rotSpeed = 120.0f;
    private int score = 0;
    public AudioClip[] jumpVoice = new AudioClip[2];
    bool isJumping = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        scale = transform.localScale;
        aSource = GetComponent<AudioSource>();

    }
    void LateUpdate()
    {

        float acc = Mathf.Max(Input.GetAxis("Vertical"), 0f);
        float rot = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Max(acc, Mathf.Abs(rot)));

        if (rot != 0)
        {
            transform.Rotate(0, rot * rotSpeed * Time.deltaTime, 0);
        }


        if (!isJumping)
        {
            if (acc != 0)
            {
                rb.velocity = transform.forward * 5.0f;
            }


            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                animator.SetTrigger("jump");
                OnJumpVoice();
            }
        }
    }

    public void OnJumpStart()
    {
        rb.velocity = transform.forward * 3f;
        rb.position = new Vector3(transform.position.x, transform.localScale.y, transform.position.z);
        transform.Rotate(0, 0, 0);

    }

    public void IsJumping(bool jump)
    {
        this.isJumping = jump;
    }

    public void OnJumpVoice()
    {
        aSource.clip = jumpVoice[Random.Range(0, 2)];
        aSource.Play();
    }

    public Vector3 GetScale()
    {
        return this.scale;
    }

    public void SetScale(Vector3 sc)
    {
        this.scale = sc;
        transform.localScale = sc;
    }

    public float GetMass()
    {
        return this.mass;
    }

    public void SetMass(float ms)
    {
        this.mass = ms;
        rb.mass = ms;
    }

    public void SetSteerActive(bool active)
    {
        rb.isKinematic = !active;
    }

    public int GetScore()
    {
        return this.score;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }
}
