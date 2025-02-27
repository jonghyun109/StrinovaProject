using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SemiZoomState : IState
{
    PlayerState state;
    private Vector3 defaultOffset;
    private float defaultFOV;

    public void EnterState(PlayerState ply)
    {
        state = ply;

        state.moveSpeed = 1.5f;


        state.cams[2].Priority = 11;
        state.anim.SetBool("IsZoom", true);

    }

    public void UpdateState()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            state.ChangeState(state.threeS);
        }
    }

    public void ExitState()
    {
        state.cams[2].Priority = 10;
        state.anim.SetBool("IsZoom", false);
        state.anim.SetTrigger("Idle");
    }

    public void Move()
    {
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) moveDir += forward;
        if (Input.GetKey(KeyCode.S)) moveDir -= forward;
        if (Input.GetKey(KeyCode.A)) moveDir -= right;
        if (Input.GetKey(KeyCode.D)) moveDir += right;

        state.player.transform.position += moveDir * state.moveSpeed * Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            state.anim.SetBool("IsShoot", true);
            state.anim.SetBool("IsZoom", false);
        }
        else
        {
            state.anim.SetBool("IsShoot", false);
            state.anim.SetBool("IsZoom", true);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state.ChangeState(state.threeS);
        }
    }
}
