using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class TwoDState : IPlayerMovement
{
    public Transform paperTF;
    public Animator animator;

    public TwoDState(Transform transform, Animator animator)
    {
        this.paperTF = transform;
        this.animator = animator;
    }

    public void Move(Transform tra, float h, float v)
    {
        tra.position += (Vector3.right * h + Vector3.up * v) * Time.deltaTime * 3;
    }

    public void PlayAnimation(float h, float v)
    {
        Vector3 direction = (v > 0 ? Vector3.back : Vector3.zero) +
                            (v < 0 ? Vector3.forward : Vector3.zero) +
                            (h < 0 ? Vector3.left : Vector3.zero) +
                            (h > 0 ? Vector3.right : Vector3.zero);

        if (direction != Vector3.zero)
        {
            paperTF.rotation = Quaternion.LookRotation(direction);
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
            paperTF.rotation = Quaternion.identity;
        }
    }
}
