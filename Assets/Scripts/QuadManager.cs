using UnityEngine;

public class QuadManager : MonoBehaviour
{
    public BoxCollider cube;  // 이동하는 플레이어
    public GameObject quadPrefab; // 기존 Quad 프리팹
    public Camera twoDCam;    // 기존 카메라
    public Camera twoDCam2;   // 새로운 카메라 (추가)

    public RenderTexture renderTexture; // 플레이어를 렌더링하는 텍스처
    private bool isQuadSpawned = false; // 새 Quad 생성 여부 체크

    void Update()
    {
        CheckAndSpawnQuad();
    }

    void CheckAndSpawnQuad()
    {
        float cubeMaxX = cube.bounds.max.x;
        float quadMaxX = quadPrefab.GetComponent<BoxCollider>().bounds.max.x;
    }

}
