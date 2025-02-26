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
        state.jumpHeight = 3.5f;
        state.cams[0].Priority = 11;
        state.player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        state.player.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void UpdateState()
    {
        if ((!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) &&
           !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)))
        {
            state.anim.SetTrigger("Idle");
        }
    }

    public void ExitState()
    {
        state.cams[0].Priority = 10;
        //state.player.SetActive(false);
    }

    public void Move()
    {
        Vector3 cameraForward = state.cams[0].transform.forward;
        Vector3 cameraRight = state.cams[0].transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        // 이동 벡터 초기화
        Vector3 moveDirection = Vector3.zero;

        // 입력에 따라 이동 방향 결정
        if (Input.GetKey(KeyCode.W))
            moveDirection += cameraForward;
        if (Input.GetKey(KeyCode.S))
            moveDirection -= cameraForward;
        if (Input.GetKey(KeyCode.A))
            moveDirection -= cameraRight;
        if (Input.GetKey(KeyCode.D))
            moveDirection += cameraRight;

        // 이동 실행
        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize();
            state.player.transform.position += moveDirection * state.moveSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {            
            state.player.transform.rotation = Quaternion.Euler(0, 40, 0);
            state.anim.SetBool("IsShoot", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            state.player.transform.rotation = Quaternion.identity;
            state.anim.SetTrigger("Idle");
            state.anim.SetBool("IsShoot", false);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {

        }

        Vector3 dir = (Input.GetKey(KeyCode.W) ? Vector3.forward : Vector3.zero) +
                      (Input.GetKey(KeyCode.S) ? Vector3.back : Vector3.zero) +
                      (Input.GetKey(KeyCode.A) ? Vector3.left : Vector3.zero) +
                      (Input.GetKey(KeyCode.D) ? Vector3.right : Vector3.zero);


        if (dir != Vector3.zero)
        {
            if (state.ischlehddh == false)
            {
                state.player.transform.rotation = Quaternion.Euler(75, dir.y, dir.z);
                state.player.gameObject.transform.localScale = new Vector3(1, 1, 0.2f);
                //state.player.transform.rotation = Quaternion.LookRotation(new Vector3(75,-dir.y,-dir.z));
            }
            else
            {
                state.player.transform.rotation = Quaternion.LookRotation(dir);
                state.anim.SetBool("IsWalk", true);
            }
        }
        else
        {
            state.anim.SetBool("IsWalk", false);
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
    //void UpdateCharacterRotation()
    //{
    //    // 카메라의 방향을 가져와서 y축만 반영
    //    Vector3 cameraForward = state.threeDCam.transform.forward;
    //    cameraForward.y = 0; // 수평 방향만 고려
    //    cameraForward.Normalize();

    //    // 캐릭터가 카메라를 등지도록 회전 설정
    //    Quaternion currentRotation = state.player.transform.rotation;
    //    Quaternion targetRotation = Quaternion.LookRotation(-cameraForward);
    //    state.player.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * 5f);
    //}
}
