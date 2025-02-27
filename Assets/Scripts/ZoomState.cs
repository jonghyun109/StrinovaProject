using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomState : IState
{
    PlayerState state;
    private float defaultFOV;

    public void EnterState(PlayerState ply)
    {
        state = ply;
        defaultFOV = state.cams[0].m_Lens.FieldOfView;

        state.crossHair.gameObject.SetActive(false);

        state.moveSpeed = 1f;

        state.cams[0].m_Lens.FieldOfView = 20f;

        state.scopeUI.SetActive(true);
    }

    public void UpdateState() { }

    public void ExitState()
    {
        state.scopeUI.SetActive(false);
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
