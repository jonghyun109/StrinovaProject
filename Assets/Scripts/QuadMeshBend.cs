using UnityEngine;

public class QuadMeshBend : MonoBehaviour
{
    //public float bendAmount = 0.5f; // 휘는 정도 (값을 조절하면 더 많이 휘어짐)
    //private Mesh mesh;
    //private Vector3[] originalVertices;

    //void Start()
    //{
    //    // Quad의 Mesh 가져오기
    //    MeshFilter meshFilter = GetComponent<MeshFilter>();
    //    if (meshFilter == null)
    //    {
    //        Debug.LogError("[QuadMeshBend] MeshFilter를 찾을 수 없음!");
    //        return;
    //    }

    //    mesh = meshFilter.mesh;
    //    originalVertices = mesh.vertices.Clone() as Vector3[];
    //}

    //void Update()
    //{
    //    BendQuad();
    //}

    //void BendQuad()
    //{
    //    Vector3[] vertices = mesh.vertices;
    //    for (int i = 0; i < vertices.Length; i++)
    //    {
    //        float offset = Mathf.Sin(vertices[i].x * Mathf.PI) * bendAmount;
    //        vertices[i].z += offset; // Z축 방향으로 휘어지게 설정
    //    }
    //    mesh.vertices = vertices;
    //    mesh.RecalculateNormals();
    //}
}
