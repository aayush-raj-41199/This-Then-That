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
    private bool isOnGround = false;
    public float positionRadius;
    public LayerMask ground, blockNormal, blockInvisible;
    public Transform playerPos;
    public Transform rightHandPos;
    public Transform leftHandPos;

    // Start is called before the first frame update
    void Start()
    {
        leftLegRb = leftLeg.GetComponent<Rigidbody2D>();
        rightLegRb = rightLeg.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                if (!GameManager.inverse)
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
                if (!GameManager.inverse)
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


        if (checkIfOnBlock(ground) || (!GameManager.inverse ? checkIfOnBlock(blockInvisible) : checkIfOnBlock(blockNormal))) 
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }


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

    bool checkIfOnBlock(LayerMask mask) 
    {
        return Physics2D.OverlapCircle(playerPos.position, positionRadius, mask) || Physics2D.OverlapCircle(rightHandPos.position, positionRadius, mask) || Physics2D.OverlapCircle(leftHandPos.position, positionRadius, mask);
    }
}
