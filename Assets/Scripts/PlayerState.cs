using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public interface IState
{
    public void EnterState(PlayerState ply){}
    public void UpdateState(PlayerState ply){}
    public void ExitState(PlayerState ply) {}
    public void Move(PlayerState ply);
}
public class PlayerState : MonoBehaviour
{
    //얘만 바꾸면 스크립트 바뀌게 ㄱ
    IState currentState;

    //3D플레이어 ,2D플레이어
    public GameObject player;
    public GameObject paperPlayer;

    //Move(wasd)
    public float moveSpeed;
    public float moveForward;
    public Vector3 moveW;
    public Vector3 moveS;
    public Vector3 moveA;
    public Vector3 moveD;
    //Animator
    public Animator anim;

    //Jump(space)
    public float jumpHeight;

    //3d에서 2d ,string 에서 2d 모드 변환 할때 객체변경
    public bool isWall = false;

    private void Start()
    {
        ChangeState(new ThreeDState());
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
            currentState.Move(this);
        }
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;
        currentState.EnterState(this);
        
    }
}
