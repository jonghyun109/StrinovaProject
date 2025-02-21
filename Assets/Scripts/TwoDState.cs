using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class TwoDState :IState
{
    PlayerState state;
    public void EnterState(PlayerState ply)
    {
        state = ply;
        state.paperPlayer.SetActive(true);
        state.moveSpeed = 2f;
        state.cams[1].Priority = 11;
    }

    public void UpdateState() { }

    public void ExitState()
    {
        state.cams[1].Priority = 10;
    }

    public void Move()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Camera.main.transform.forward - Camera.main.transform.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += -Camera.main.transform.forward - Camera.main.transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += -Camera.main.transform.forward + Camera.main.transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Camera.main.transform.right;
        }

        direction.y = 0;
        state.player.transform.position += direction.normalized * state.moveSpeed * Time.deltaTime;
    }
}