using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMode : MonoBehaviour
{
    public GameObject player;
    public PlayerMove twoDCam;
    public CinemachineVirtualCamera[] cams;
    static bool isString = false;

    void Update()
    {
        StringModeOnOff();
    }

    void StringModeOnOff()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isString = !isString;
            if (isString)
            {
                player.gameObject.transform.localScale = new Vector3(1, 1, 0.2f);
                player.gameObject.transform.rotation = Quaternion.Euler(0, 110, 0);
                cams[0].Priority = 10;
                cams[1].Priority = 10;
                cams[2].Priority = 11;
                

            }
            else
            {
                player.gameObject.transform.localScale = new Vector3(1, 1, 1);
                player.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                cams[0].Priority = 11;
                cams[1].Priority = 10;
                cams[2].Priority = 10;
            }
        }
        if(twoDCam.hasSpawned == true)
        {
            cams[0].Priority = 10; 
            cams[1].Priority = 11; 
            cams[2].Priority = 10; 
        }
    }
}
