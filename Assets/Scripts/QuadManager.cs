using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadManager : MonoBehaviour
{
    public GameObject quadPrefab; // 붙일 Quad 프리팹

    // Quad를 붙이는 함수
    public void AttachQuad(Vector3 hitPoint, Vector3 normal)
    {
        // Quad를 부착할 위치 계산 (벽의 표면에서 살짝 떨어진 위치)
        Vector3 quadPosition = hitPoint + (normal * 0.501f);

        // Quad의 회전 설정 (벽과 평행하게 부착)
        Quaternion quadRotation = Quaternion.LookRotation(-normal);

        // Quad 생성
        Instantiate(quadPrefab, quadPosition, quadRotation);

        Debug.Log("Quad 생성됨! 위치: " + quadPosition);
    }
}
