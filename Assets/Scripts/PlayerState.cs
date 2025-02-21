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
    //�길 �ٲٸ� ��ũ��Ʈ �ٲ�� ��
    IState currentState;

    //3D�÷��̾� ,2D�÷��̾�
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
        
    //3d���� 2d ,string ���� 2d ��� ��ȯ �Ҷ� ��ü����
    public bool isWall = false;

    //string ���
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
