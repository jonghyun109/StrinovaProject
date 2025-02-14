using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class TwoDMode : MonoBehaviour
{
    public AnimatorController controller;
    Animator anim;
    public GameObject paper;

    private void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            anim.SetBool("IsRunning", true);
            paper.transform.position += Vector3.up*Time.deltaTime*3;
        }
        if(Input.GetKey(KeyCode.S))
        {
            paper.transform.position += Vector3.down*Time.deltaTime * 3;
        }
        if(Input.GetKey(KeyCode.A))
        {
            paper.transform.position += Vector3.left*Time.deltaTime * 3;
        }
        if(Input.GetKey(KeyCode.D))
        {
            paper.transform.position += Vector3.right * Time.deltaTime * 3;
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }
}
