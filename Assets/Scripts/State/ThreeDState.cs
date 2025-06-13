using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;

public class ThreeDState : IState
{
    PlayerState state;
    public void EnterState(PlayerState ply)
    {
        state = ply;
        state.moveSpeed = 3f;
        state.jumpHeight = 30f;
        state.cams[0].Priority = 11;
        state.cams[0].m_Lens.FieldOfView = 60f;
        state.cams[0].m_Lens.NearClipPlane = 0.1f;

        state.crossHair.gameObject.SetActive(true);
        state.player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        state.player.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        state.anim.SetLayerWeight(1, 0);
        state.anim.SetLayerWeight(2, 1);


    }

    public void UpdateState()
    {
        if(Input.GetKey(KeyCode.W))
        {
            state.anim.SetBool("IsRun", true);
            state.anim.SetBool("IsBackward", false);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            state.anim.SetBool("IsBackward", true);
            state.anim.SetBool("IsRun", false);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            state.anim.SetBool("IsLeftStep", true);
            state.anim.SetBool("IsRightStep", false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            state.anim.SetBool("IsRightStep", true);
            state.anim.SetBool("IsLeftStep", false);
        }
        else
        {
            state.anim.SetBool("IsRightStep", false);
            state.anim.SetBool("IsLeftStep", false);
            state.anim.SetBool("IsRun", false);
            state.anim.SetBool("IsBackward", false);
            state.anim.SetTrigger("Idle");
        }
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && state.jumpCount < 2)
        {
            state.jumpCount++;
            state.anim.SetTrigger("Jump");
            state.rb.AddForce(Vector3.up * state.jumpHeight, ForceMode.Impulse);
        }
    }

    public void ExitState()
    {
        state.cams[0].Priority = 10;
        state.anim.SetTrigger("Idle");
    }

    public void Move()
    {
        Vector3 cameraForward = state.cams[0].transform.forward;
        Vector3 cameraRight = state.cams[0].transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += cameraForward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= cameraForward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= cameraRight;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += cameraRight;
        }

        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize();
            state.player.transform.position += moveDirection * state.moveSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            state.moveSpeed = 2f;
            state.anim.SetBool("IsShoot", true);

        }
        if (Input.GetMouseButtonUp(0))
        {
            state.moveSpeed = 3f;
            state.anim.SetTrigger("Idle");
            state.anim.SetBool("IsShoot", false);
        }


        state.player.transform.position += moveDirection * state.moveSpeed * Time.deltaTime;        
    }        
}
    

