using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerStringMode : MonoBehaviour
{
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
                gameObject.transform.localScale = new Vector3(1, 1, 0.2f);
                gameObject.transform.rotation = Quaternion.Euler(0, 110, 0);
                cams[0].Priority = 9;

            }
            else
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                cams[0].Priority = 11;
            }
        }
    }
}
