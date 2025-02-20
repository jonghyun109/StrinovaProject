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
        state.paperPlayer.SetActive(true);
    }

    public void UpdateState(PlayerState ply) { }

    public void ExitState(PlayerState ply) { }

    public void Move(PlayerState ply)
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
        ply.player.transform.position += direction.normalized * ply.moveSpeed * Time.deltaTime;
    }
}