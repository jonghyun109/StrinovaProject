using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringState : IState
{
    PlayerState state;
    public void EnterState(PlayerState ply) 
    {
        state = ply;
        state.moveSpeed = 1.5f;
        state.cams[0].Priority = 11;
        state.anim.SetLayerWeight(1, 1);
        state.anim.SetLayerWeight(2, 0);
    }

    public void UpdateState()
    {
        if (Input.GetKey(KeyCode.W))
        {
            state.anim.SetBool("IsLeftStep", true);
            state.anim.SetBool("IsRightStep", false);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            state.anim.SetBool("IsBackward", true);
            state.anim.SetBool("IsWalk", false);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            state.anim.SetBool("IsRightStep", true);
            state.anim.SetBool("IsLeftStep", false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            state.anim.SetBool("IsWalk", true);
            state.anim.SetBool("IsBackward", false);
        }
        else
        {
            state.anim.SetBool("IsRightStep", false);
            state.anim.SetBool("IsLeftStep", false); 
            state.anim.SetBool("IsWalk", false);
            state.anim.SetBool("IsBackward", false);
            state.anim.SetTrigger("Idle");
        }
    }

    public void ExitState()
    {
        state.cams[0].Priority = 10;
        state.player.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (state.currentState is not StringState)
        {
            state.player.transform.localScale = Vector3.one;
        }
    }

    public void Move()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // y축 값을 0으로 고정하여 2D 이동 방식으로 변환
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += right;
        }
        state.player.transform.position += direction.normalized * state.moveSpeed * Time.deltaTime;

        if(Input.GetMouseButtonDown(0))
        {
            state.ChangeState(state.threeS);
        }
        //if(Input.GetKeyUp(KeyCode.LeftControl))
        //{
        //    state.ChangeState(state.threeS);
        //}
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state.ChangeState(state.threeS);
        }
    }
}
