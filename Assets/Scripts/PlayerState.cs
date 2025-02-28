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
    public IState currentState;

    [Header("플레이어")]
    //3D플레이어 ,2D플레이어
    public GameObject player;
    public GameObject paperPlayer;

    //플레이어 변환할 상태들
    public StringState stringS = new StringState();
    public ThreeDState threeS = new ThreeDState();
    public TwoDState twoS = new TwoDState();
    public ZoomState zoomS = new ZoomState();
    public SemiZoomState semiZoomS = new SemiZoomState();

    [Header("움직임")]
    //Move
    public float moveSpeed;
    public float moveForward;

    [Header("애니메이션")]
    //Animator
    public Animator anim;

    [Header("점프")]
    //Jump(space)
    public float jumpHeight;
    public int jumpCount;
    public Rigidbody rb;

    [Header("")]
    //3d에서 2d ,string 에서 2d 모드 변환 할때 객체변경
    public bool isWall = false;
    public bool hasSpawned = false;

    [Header("카메라, UI")]

    // 카메라 및 UI 관련
    public CinemachineVirtualCamera[] cams;
    public GameObject scopeUI;
    public GameObject crossHair;

    [Header("stringMode")]
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
        HandleSemiZoom();
    }
    
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState(this);

        if (newState is StringState)
        {
            player.transform.rotation = Quaternion.Euler(0, 110, 0);
            player.transform.localScale = new Vector3(1, 1, 0.2f);
        }
        else
        {
            player.transform.localScale = Vector3.one;
        }
    }

    void StringModeOnOff()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {            
            ChangeState(stringS);          
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {            
            ChangeState(threeS);            
        }
    }
    void HandleZoom()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (currentState is ZoomState)
            {
                ChangeState(threeS);
            }
            else
            {
                ChangeState(zoomS);
            }
        }
    }
    void HandleSemiZoom()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentState is ThreeDState)
        {
            ChangeState(semiZoomS); 
        }
    }

}
