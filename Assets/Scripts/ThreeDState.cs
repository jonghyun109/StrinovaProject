using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDState : MonoBehaviour , IPlayerMovement
{
    public GameObject threeD;
    private Animator animator;

    private void Start()
    {
        animator = threeD.GetComponent<Animator>();
    }
    public void Move(Transform transform, float h, float v)
    {
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        transform.position += moveDir.normalized * Time.deltaTime * 3;
    }

    public void PlayAnimation(float h, float v)
    {
        if (v > 0.1f)
        {
            animator.SetBool("IsForward", true);
        }
        else
        {
            animator.SetBool("IsForward", false);
        }

        if (v < -0.1f)
        {
            animator.SetBool("IsBackward", true);
        }
        else
        { 
            animator.SetBool("IsBackward", false);
        }

        if (h > 0.1f)
        {
            animator.SetBool("IsRightstep", true);
        }
        else
        {
            animator.SetBool("IsRightstep", false);
        }

        if (h < -0.1f)
        {
            animator.SetBool("IsLeftStep", true);
        }
        else
        {
            animator.SetBool("IsLeftStep", false);
        }

        if (h == 0 && v == 0)
        {
            animator.SetTrigger("Idle");
        }
    }
}
