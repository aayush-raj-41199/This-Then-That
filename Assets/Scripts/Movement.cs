using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject leftLeg;
    public GameObject rightLeg;
    Rigidbody2D leftLegRb;
    Rigidbody2D rightLegRb;
    public Rigidbody2D Rb;

    public Animator anim;

    [SerializeField] float speed = 1.5f;
    [SerializeField] float stepWait = 0.5f;
    [SerializeField] float jumpForce = 10f;
    private bool isOnGround;
    public float positionRadius;
    public LayerMask ground;
    public Transform playerPos;
    public bool isInverse = false;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        leftLegRb = leftLeg.GetComponent<Rigidbody2D>();
        rightLegRb = rightLeg.GetComponent<Rigidbody2D>();
        if(gameManager.GetComponent<GameManager>().inverse)
        {
            isInverse = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                if (!isInverse)
                {
                    anim.Play("WalkRight");
                    StartCoroutine(MoveRight(stepWait));
                }
                else
                {
                    anim.Play("WalkLeft");
                    StartCoroutine(MoveLeft(stepWait));
                }
            }
            else
            {
                if (!isInverse)
                {
                    anim.Play("WalkLeft");
                    StartCoroutine(MoveLeft(stepWait));
                }
                else
                {
                    anim.Play("WalkRight");
                    StartCoroutine(MoveRight(stepWait));
                }
            }
        }
        else
        {
            anim.Play("Idle");
        }

        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);
        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            Rb.AddForce(Vector2.up * jumpForce);
        }

    }

    IEnumerator MoveRight(float seconds)
    {
        leftLegRb.AddForce(Vector2.right * (speed * 1000) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        rightLegRb.AddForce(Vector2.right * (speed * 1000) * Time.deltaTime);
    }

    IEnumerator MoveLeft(float seconds)
    {
        rightLegRb.AddForce(Vector2.left * (speed * 1000) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        leftLegRb.AddForce(Vector2.left * (speed * 1000) * Time.deltaTime);
    }
}
