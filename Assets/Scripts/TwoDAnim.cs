using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDAnim : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    void Update()
    {
        TwoDDirection();
    }
    void TwoDDirection()
    {
        Vector3 direction = (Input.GetKey(KeyCode.W) ? Vector3.back : Vector3.zero) +
                            (Input.GetKey(KeyCode.S) ? Vector3.forward : Vector3.zero) +
                            (Input.GetKey(KeyCode.A) ? Vector3.right : Vector3.zero) +
                            (Input.GetKey(KeyCode.D) ? Vector3.left : Vector3.zero);

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
            gameObject.transform.rotation = Quaternion.identity;
        }
    }
}
