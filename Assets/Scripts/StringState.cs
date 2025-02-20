using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringState : IPlayerMovement
{
    public Transform stringTF;
    public Animator animator;

    public StringState(Transform transform, Animator animator)
    {
        this.stringTF = transform;
        this.animator = animator;
    }

    public void Move(Transform transform, float h, float v)
    {
        Transform camTransform = Camera.main.transform; // 메인 카메라 가져오기
        Vector3 forward = camTransform.forward; // 카메라의 전방 벡터
        Vector3 right = camTransform.right;     // 카메라의 오른쪽 벡터

        forward.y = 0; // 수직 이동을 막기 위해 y축을 0으로 설정
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * v + right * h;
        transform.position += moveDirection * Time.deltaTime * 1.5f;
    }


    public void PlayAnimation(float h, float v)
    {
        Transform camTransform = Camera.main.transform;
        Vector3 forward = camTransform.forward;
        Vector3 right = camTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * v + right * h;

        if (moveDirection != Vector3.zero)
        {
            stringTF.rotation = Quaternion.LookRotation(moveDirection); // 카메라 기준으로 방향 회전
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
}
