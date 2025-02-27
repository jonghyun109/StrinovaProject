using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomState : IState
{
    PlayerState state;

    public void EnterState(PlayerState ply)
    {
        state = ply;
        state.cams[1].Priority = 11;
        state.crossHair.gameObject.SetActive(false);
        state.player.transform.rotation = Quaternion.Euler(0, 40, 0);
        state.moveSpeed = 1f;
        state.anim.SetBool("IsZoom", true);

        state.scopeUI.SetActive(true);
    }

    public void UpdateState() { }

    public void ExitState()
    {
        state.scopeUI.SetActive(false);
        state.cams[1].Priority = 10;
        state.anim.SetBool("IsZoom", false);
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
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state.ChangeState(state.threeS);
        }
    }
}
