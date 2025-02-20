using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class PlayerMove : MonoBehaviour 
{
    [SerializeField]
    GameObject TwoDChan;
    [SerializeField]
    GameObject twoDPrefab;

    Transform tr;
    Rigidbody rb;
    Animator animator;

    public float turnSpeed = 80.0f;
    float moveSpeed = 50;
    float jumpHeight = 5f;
    int jumpCount = 0;

    float h;
    float v;
    float mouseX;
    float mouseY;

    Vector3 spawnPosition;
    public bool hasSpawned = false;

    public CinemachineVirtualCamera twoDcam;

    public float hp { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void Start()
    {
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; //물리 시, 회전 방지

        TwoDChan.gameObject.SetActive(false);
    }
    void PlayerAnim(float h, float v)
    {
        if (v >= 0.1f)
        {
            moveSpeed = 3;
            animator.SetBool("IsForward", true);

            animator.SetBool("IsBackward", false);
            animator.SetBool("IsRightstep", false);
            animator.SetBool("IsLeftStep", false);

        }
        else if (v <= -0.1f)
        {
            moveSpeed = 1.5f;
            animator.SetBool("IsBackward", true);

            animator.SetBool("IsRightstep", false);
            animator.SetBool("IsLeftStep", false);
            animator.SetBool("IsForward", false);
        }
        else if (h >= 0.1f)
        {
            moveSpeed = 2;
            animator.SetBool("IsRightstep", true);

            animator.SetBool("IsLeftStep", false);
            animator.SetBool("IsForward", false);
            animator.SetBool("IsBackward", false);
        }
        else if (h <= -0.1f)
        {
            moveSpeed = 2;
            animator.SetBool("IsLeftStep", true);

            animator.SetBool("IsForward", false);
            animator.SetBool("IsBackward", false);
            animator.SetBool("IsRightstep", false);
        }
        else
        {
            moveSpeed = 3;
            animator.SetBool("IsForward", false);

            animator.SetBool("IsBackward", false);
            animator.SetBool("IsRightstep", false);
            animator.SetBool("IsLeftStep", false);
            animator.SetTrigger("Idle");
        }
    }
    void PlayerMoving()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");


        Vector3 moveDirection = (Vector3.forward * v) + (Vector3.right * h);

        tr.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * mouseX);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            jumpCount++;
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
    void Update()
    {
        PlayerMoving();
        PlayerAnim(h, v);



        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsAttack",true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAttack", false);
        }

        
    }
    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.CompareTag("Floor"))
        {
            animator.SetTrigger("Idle");
            jumpCount = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //2D Mode로 변환
        if (!hasSpawned && Input.GetKeyDown(KeyCode.E))
        {
            TwoDChan.gameObject.SetActive(true);
            if (other.gameObject.CompareTag("TwoDWall"))
            {
                spawnPosition = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
                var twoD = Instantiate(twoDPrefab, spawnPosition, Quaternion.identity);

                hasSpawned = true; // 다시 소환되지 않도록 설정
                //잠깐 지워놓기
                gameObject.SetActive(false);
                //카메라 전환
                twoDcam.LookAt = twoD.transform;
                twoDcam.Follow = twoD.transform;
            }
        }
    }

    public void PlayAnimation(float h, float v)
    {
        throw new System.NotImplementedException();
    }
}
