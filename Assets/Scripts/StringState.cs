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
        state.cams[2].Priority = 11;
        state.player.gameObject.transform.localScale = new Vector3(1, 1, 0.2f);
        state.player.gameObject.transform.rotation = Quaternion.Euler(0, 110, 0);
    }

    public void UpdateState() { }

    public void ExitState()
    {
        state.cams[2].Priority = 10;
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

        // 이동 적용
        state.player.transform.position += direction.normalized * state.moveSpeed * Time.deltaTime;
    }

}
