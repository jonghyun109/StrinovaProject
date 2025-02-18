using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadManager : MonoBehaviour
{
    public GameObject quadPrefab; // ���� Quad ������

    // Quad�� ���̴� �Լ�
    public void AttachQuad(Vector3 hitPoint, Vector3 normal)
    {
        // Quad�� ������ ��ġ ��� (���� ǥ�鿡�� ��¦ ������ ��ġ)
        Vector3 quadPosition = hitPoint + (normal * 0.501f);

        // Quad�� ȸ�� ���� (���� �����ϰ� ����)
        Quaternion quadRotation = Quaternion.LookRotation(-normal);

        // Quad ����
        Instantiate(quadPrefab, quadPosition, quadRotation);

        Debug.Log("Quad ������! ��ġ: " + quadPosition);
    }
}
