using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDState : IState
{
    PlayerState state;
    public void EnterState(PlayerState ply)
    {
        state = ply;
        state.moveSpeed = 3f;
        state.jumpHeight = 3f;
        state.cams[0].Priority = 11;
        state.player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        state.player.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void UpdateState()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            state.anim.SetBool("IsWalk", true);            
        }
        else
        {
            state.anim.SetTrigger("Idle");
            state.anim.SetBool("IsWalk", false);
        }
    }

    public void ExitState() 
    {
        state.cams[0].Priority = 10;
        //state.player.SetActive(false);
    }
    public void Move()
    {
        state.moveSpeed = 3f;
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {           
            direction += Camera.main.transform.forward;
            if(Input.GetKey(KeyCode.LeftShift))
            {
                state.moveSpeed = 5f;
                state.anim.SetBool("IsForward",true);
                state.anim.SetBool("IsWalk", false);
            }
            else
            {
                state.anim.SetBool("IsWalk", true);
                state.anim.SetBool("IsForward", false);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= Camera.main.transform.forward;
            state.moveSpeed = 3f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= Camera.main.transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Camera.main.transform.right;
        }        
        if (Input.GetKeyDown(KeyCode.Space) && state.jumpCount < 2)
        {
            state.jumpCount++;
            state.anim.SetTrigger("Jump");
            state.rb.AddForce(Vector3.up * state.jumpHeight, ForceMode.Impulse);
        }
        

        state.player.transform.position += direction.normalized * state.moveSpeed * Time.deltaTime;        
    }
}
