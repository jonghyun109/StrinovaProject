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


        if (cubeMaxX >= quadMaxX - 0.1f && !isQuadSpawned)
        {
            SpawnNewQuad();
            isQuadSpawned = true;
        }
    }

    void SpawnNewQuad()
    {
        BoxCollider quadCollider = quadPrefab.GetComponent<BoxCollider>();
        if (quadCollider == null)
        {
            return;
        }

        // 새로운 Quad를 기존 Quad의 끝에 생성
        Vector3 newQuadPosition = quadPrefab.transform.position + new Vector3(quadCollider.bounds.size.x, 0, 0);
        GameObject newQuad = Instantiate(quadPrefab, newQuadPosition, quadPrefab.transform.rotation);

        if (newQuad == null)
        {
            return;
        }

        if (!newQuad.GetComponent<QuadMeshBend>())
        {
            newQuad.AddComponent<QuadMeshBend>();
        }

        // 🟢 렌더 텍스처 적용
        Renderer quadRenderer = newQuad.GetComponent<Renderer>();
        if (quadRenderer != null && renderTexture != null)
        {
            quadRenderer.material = new Material(quadRenderer.material);
            quadRenderer.material.mainTexture = renderTexture;
        }
        else
        {
        }

        // 🟢 RecognizeWall에 새로운 Quad 등록
        RecognizeWall recognizeWall = FindObjectOfType<RecognizeWall>();
        if (recognizeWall != null)
        {
            recognizeWall.SetNewPaperPlayer(newQuad);
        }
        else
        {
            Debug.Log("d");
        }
    }
}
