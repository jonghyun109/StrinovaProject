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
    //�길 �ٲٸ� ��ũ��Ʈ �ٲ�� ��
    IState currentState;

    //3D�÷��̾� ,2D�÷��̾�
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

    //3d���� 2d ,string ���� 2d ��� ��ȯ �Ҷ� ��ü����
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
