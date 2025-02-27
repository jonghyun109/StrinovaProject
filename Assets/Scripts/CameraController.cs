using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraController : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public CinemachineVirtualCamera virtualCam;
    public float mouseSensitivity = 2f; // 마우스 감도
    public Vector3 offset = new Vector3(0, 3, -5); // 카메라 오프셋

    private float rotationX = 0f;
    private float rotationY = 0f;
    private bool isShooting = false; // 총 쏘는 상태 체크


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 잠금
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 회전 값 갱신
        rotationX -= mouseY;
        rotationY += mouseX;
        rotationX = Mathf.Clamp(rotationX, -45f, 45f); // 위아래 제한

        // 카메라 회전 적용 (플레이어는 좌우 회전만 적용)
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.position = player.position + transform.rotation * offset;

        // 플레이어가 카메라 방향을 바라보도록 설정 (좌우만)
        player.rotation = Quaternion.Euler(0, rotationY, 0);

        if (Input.GetMouseButton(0)|| Input.GetMouseButton(1))
        {
            player.rotation = Quaternion.Euler(0, rotationY+40, 0);
        }
        
        else
        {
            // 플레이어가 카메라 방향을 따라 회전
            player.rotation = Quaternion.Euler(0, rotationY, 0);
        }        
    }
    
}


