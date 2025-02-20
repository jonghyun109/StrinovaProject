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
        Transform camTransform = Camera.main.transform; // ���� ī�޶� ��������
        Vector3 forward = camTransform.forward; // ī�޶��� ���� ����
        Vector3 right = camTransform.right;     // ī�޶��� ������ ����

        forward.y = 0; // ���� �̵��� ���� ���� y���� 0���� ����
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
            stringTF.rotation = Quaternion.LookRotation(moveDirection); // ī�޶� �������� ���� ȸ��
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
}
