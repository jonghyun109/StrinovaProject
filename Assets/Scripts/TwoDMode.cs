using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class TwoDMode : MonoBehaviour , IPlayerMovement
{
    public GameObject paper;

    public void MoveW(Transform tra) 
    {
        tra.transform.position += Vector3.up * Time.deltaTime * 3;
    }
    public void MoveS(Transform tra)
    {
        tra.transform.position += Vector3.down * Time.deltaTime * 3;
    }
    public void MoveA(Transform tra) 
    {
        tra.transform.position += Vector3.left * Time.deltaTime * 3;
    }
    public void MoveD(Transform tra)
    {
        tra.transform.position += Vector3.right * Time.deltaTime * 3;
    }

    //void paperWallTurn()
    //{
    //    //Quad ��ǥ
    //    Bounds bounds = paper.GetComponent<Renderer>().bounds;
        
    //    //Cube ��ǥ

    //    //quad �� cube �ո� �ִ� ��ǥ ��
    //    //quad�� cube �ո��� �ִ� ��ǥ�� �Ѿ�� ���鿡 ���� ���� quad����
        
        
    //    if(bounds.Contains(paper.transform.position))
    //    {
    //        Debug.Log(bounds);
    //    }
    //}
    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) MoveW(paper.transform);
        if (Input.GetKey(KeyCode.S)) MoveS(paper.transform);
        if (Input.GetKey(KeyCode.A)) MoveA(paper.transform);
        if (Input.GetKey(KeyCode.D)) MoveD(paper.transform);
    }
}
