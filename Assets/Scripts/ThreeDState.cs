using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDState : IState
{
    PlayerState state;
    public void EnterState(PlayerState ply)
    {
        state = ply;
        state.moveSpeed = 2f;
        state.moveForward = 2f;

        state.moveW = Vector3.forward;
        state.moveS = Vector3.back;
        state.moveA = Vector3.left;
        state.moveD = Vector3.right;
    }

    public void UpdateState(PlayerState ply) { }

    public void ExitState(PlayerState ply) 
    {
    }
    public void Move(PlayerState ply)
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Camera.main.transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= Camera.main.transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        { 
            direction -= Camera.main.transform.right; 
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Camera.main.transform.right;
        }

        direction.y = 0;
        ply.player.transform.position += direction.normalized * ply.moveSpeed * Time.deltaTime;
        
    }
}
