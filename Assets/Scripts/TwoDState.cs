using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Playables;

public class TwoDState : IState
{
    PlayerState state;
    public void EnterState(PlayerState ply)
    {
        state = ply;
        state.paperPlayer.SetActive(true);
        state.moveSpeed = 3f;
        state.cams[1].Priority = 11;
        
        //state.crossHair.SetActive(false);
    }

    public void UpdateState() { }

    public void ExitState()
    {
        state.cams[1].Priority = 10;
        state.paperPlayer.SetActive(false);
    }

    public void Move()
    {
        Vector3 up = Vector2.up;
        Vector3 right = Vector2.right;

        Vector3 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dir += up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir -= right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir -= up;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += right;
        }

        state.paperPlayer.transform.position += dir * state.moveSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state.ChangeState(state.threeS);
            state.jumpCount = 0;
            state.player.transform.position 
                = new Vector3(state.paperPlayer.transform.position.x, state.paperPlayer.transform.position.y - 1, state.paperPlayer.transform.position.z);
            state.player.SetActive(true);

            state.rb.velocity = Vector3.zero;
        }
        
    }
}