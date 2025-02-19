using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringState : MonoBehaviour, IPlayerMovement
{
    public GameObject stringMode;
    private Animator animator;

    private void Start()
    {
        animator = stringMode.GetComponent<Animator>();
    }

    public void Move(Transform transform, float h, float v)
    {
        transform.position += (Vector3.forward * v + Vector3.right * h) * Time.deltaTime * 1.5f;
    }

    public void PlayAnimation(float h, float v)
    {
        Vector3 direction = (v > 0 ? Vector3.left : Vector3.zero) +
                            (v < 0 ? Vector3.right : Vector3.zero) +
                            (h < 0 ? Vector3.back : Vector3.zero) +
                            (h > 0 ? Vector3.forward : Vector3.zero);

        if (direction != Vector3.zero)
        {
            stringMode.transform.rotation = Quaternion.LookRotation(direction);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
            stringMode.transform.rotation = Quaternion.identity;
        }
    }
}
