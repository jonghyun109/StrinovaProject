using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public interface IState
{
    public void EnterState(PlayerState ply);
    public void UpdateState();
    public void ExitState();
    public void Move();
    public void Jump();    
}
public class PlayerState : MonoBehaviour
{
    //얘만 바꾸면 스크립트 바뀌게 ㄱ
    [SerializeField]
    IState currentState;

    //3D플레이어 ,2D플레이어
    public GameObject player;
    public GameObject paperPlayer;

    //플레이어 변환할 상태들
    public StringState stringS = new StringState();
    public ThreeDState threeS = new ThreeDState();
    public TwoDState twoS = new TwoDState();
    public ZoomState zoomS = new ZoomState();

    //Move
    public float moveSpeed;
    public float moveForward;

    //Animator
    public Animator anim;    

    //Jump(space)
    public float jumpHeight;
    public int jumpCount;
    public Rigidbody rb;
        
    //3d에서 2d ,string 에서 2d 모드 변환 할때 객체변경
    public bool isWall = false;
    public bool hasSpawned = false;
    public GameObject zoom;
    public GameObject crossHair;

    
    // 카메라 및 UI 관련
    public CinemachineVirtualCamera[] cams;
    public GameObject scopeUI;

    //string 모드
    static bool isString = false;
    public bool ischlehddh = true;
    

    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        ChangeState(new ThreeDState());
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Move();
            currentState.Jump();
            currentState.UpdateState();
        }
        if(hasSpawned)
        {
            hasSpawned = false;
            ChangeState(twoS);
        }
        StringModeOnOff();
        HandleZoom();
    }
    
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState(this);        
    }

    void StringModeOnOff()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (jumpCount > 0 && Input.GetKey(KeyCode.LeftControl))
            {
                ischlehddh = false;
                anim.SetBool("IsFlying", true);                
                rb.drag = 5f;
            }
            else
            {
                isString = !isString;
                if (isString)
                {
                    ChangeState(stringS);
                }
                else
                {
                    ChangeState(threeS);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ischlehddh = true;
        }
    }
    void HandleZoom()
    {
        if (Input.GetMouseButtonDown(1)) // 우클릭
        {
            if (currentState is ZoomState)
            {
                ChangeState(threeS); // 줌 상태일 때 해제
            }
            else
            {
                ChangeState(zoomS); // 줌 상태로 변경
            }
        }
    }

}
