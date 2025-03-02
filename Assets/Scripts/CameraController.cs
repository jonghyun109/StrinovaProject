using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class CameraController : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public CinemachineVirtualCamera virtualCam;
    public float mouseSensitivity = 2f; // 마우스 감도 (기본값 2f)
    public Vector3 offset = new Vector3(0, 3, -5); // 카메라 오프셋

    private float rotationX = 0f;
    private float rotationY = 0f;
    private bool isShooting = false; // 총 쏘는 상태 체크

    public bool isPaused = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 잠금
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (isPaused) return; // ESC 메뉴가 열려있으면 카메라 멈춤

        // 마우스 감도가 즉시 반영되도록 적용
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 회전 값 갱신
        rotationX -= mouseY;
        rotationY += mouseX ;
        rotationX = Mathf.Clamp(rotationX, -45f, 45f); // 위아래 제한

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.position = player.position + transform.rotation * offset;

        player.rotation = Quaternion.Euler(0, rotationY, 0);

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftShift))
        {
            player.rotation = Quaternion.Euler(0, rotationY + 40, 0);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            player.rotation = Quaternion.Euler(0, rotationY + 110, 0);
        }
        else
        {
            // 플레이어가 카메라 방향을 따라 회전
            player.rotation = Quaternion.Euler(0, rotationY, 0);
        }
    }

    // GameManager에서 마우스 감도 설정할 수 있도록 메서드 추가
    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSensitivity = sensitivity;
    }
}
