using System.Collections;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    public Transform cameraTransform; // 카메라(또는 총) Transform
    public float recoilAmount = 1f;  // 반동 크기 (작게 설정)
    public float recoilSpeed = 5f;   // 반동 복구 속도

    private Quaternion originalRotation; // 원래 회전 값 저장
    private bool isRecoiling = false;

    void Start()
    {
        originalRotation = cameraTransform.localRotation; // 초기 회전값 저장
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 좌클릭하면 반동 적용
        {
            ApplyRecoil();
        }

        if (isRecoiling) // 반동 복구
        {
            cameraTransform.localRotation = Quaternion.Lerp(cameraTransform.localRotation, originalRotation, Time.deltaTime * recoilSpeed);
            if (Quaternion.Angle(cameraTransform.localRotation, originalRotation) < 0.1f)
            {
                cameraTransform.localRotation = originalRotation; // 거의 복구되면 원래 회전값으로 설정
                isRecoiling = false;
            }
        }
    }

    void ApplyRecoil()
    {
        if (isRecoiling) return; // 반동 중이면 중복 실행 방지

        Quaternion recoilRotation = Quaternion.Euler(-recoilAmount, 0, 0) * cameraTransform.localRotation;
        cameraTransform.localRotation = recoilRotation;
        isRecoiling = true;
    }
}
