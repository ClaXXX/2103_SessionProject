using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    public int mapXSize = 2;
    public int mapZSize = 2;

    // Start is called before the first frame update
    void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        createShape();
        updateMesh();
    }

    void createShape() {
        vertices = new Vector3[(mapXSize + 1) * (mapZSize + 1)];

        int a = 0;

        float test1 = Random.Range(1, 15) / 10f;
        float test2 = Random.Range(1, 15) / 10f;
        
        for (int i = 0; i <= mapXSize; i++) {
            for (int j = 0; j <= mapZSize; j++) {
                float y = Mathf.PerlinNoise(i * test1, j * test2);
                vertices[a] = new Vector3(j, y, i);
                a++;
            }
        }

        triangles = new int[mapXSize * mapZSize * 6];
        
        int vertex = 0;
        int triangle = 0;

        for (int j = 0; j < mapZSize; j++) {
            for (int i = 0; i < mapXSize; i++) {
                triangles[triangle] = vertex;
                triangles[triangle +1] = vertex + mapXSize + 1;
                triangles[triangle + 2] = vertex + 1;
                triangles[triangle + 3] = vertex + 1;
                triangles[triangle + 4] = vertex + mapXSize + 1;
                triangles[triangle + 5] = vertex + mapXSize + 2;

                vertex++;
                triangle += 6;
            }
            vertex++;
        }
    }

    void updateMesh() {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        mesh.RecalculateNormals();
    }
    
    private void OnDrawGizmos() {
        if (vertices == null) {
            return;
        }
        for (int i = 0; i < vertices.Length; i++) {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
