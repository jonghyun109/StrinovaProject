using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadManager : MonoBehaviour
{
    public BoxCollider cube;
    public BoxCollider quad;
    public Camera twoDCam;
    
    void TurnTwoDWall()
    {
        if(cube.bounds.max.x < quad.bounds.center.x)
        {
            twoDCam.transform.position = Vector3.left;
        }
    }

}
