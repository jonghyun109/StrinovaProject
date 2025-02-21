using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public interface IState
{
    public void EnterState(PlayerState ply);
    public void UpdateState();
    public void ExitState();
    public void Move();
}
public class PlayerState : MonoBehaviour
{
    //얘만 바꾸면 스크립트 바뀌게 ㄱ
    IState currentState;

    //3D플레이어 ,2D플레이어
    public GameObject player;
    public GameObject paperPlayer;

    StringState SS = new StringState();
    ThreeDState TS = new ThreeDState();
    //Move(wasd)
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

    //string 모드
    public CinemachineVirtualCamera[] cams;
    static bool isString = false;
    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        ChangeState(new ThreeDState());
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
            currentState.Move();
        }
        StringModeOnOff();
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
            isString = !isString;
            if (isString)
            {
                ChangeState(SS);                
            }
            else
            {
                ChangeState(TS);
            }
        }
    }
    
}
