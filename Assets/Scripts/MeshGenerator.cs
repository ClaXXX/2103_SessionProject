﻿using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {
    private Mesh mesh;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private GameObject holePrefab;
    [SerializeField] private Transform holePosition;
    private Vector3[] vertices;
    private int[] triangles;

    public int mapXSize = 2;
    public int mapZSize = 2;

    private bool foundPuttingZone = false;
    private int[] puttingHoleVertices = new int[16];

    // Start is called before the first frame update
    void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        createShape();
        updateMesh();
        instanciatePrefabs();
    }

    void createShape() {
        vertices = new Vector3[(mapXSize + 1) * (mapZSize + 1)];

        int a = 0;

        float test1 = Random.Range(1, 9) / 10f;
        float test2 = Random.Range(1, 4) / 10f; // TODO : Faire que le système génère un numbre aléatoire pour décider de la direction des vauges
        
        // TODO : Choisir la position du drapeau et du point de départ
        int puttingHoleX = Random.Range(0, mapXSize - 4);
        int puttingHoleZ = Random.Range(0, mapZSize - 4);

        for (int i = 0; i <= mapXSize; i++) {
            for (int j = 0; j <= mapZSize; j++) {
                float y = 0;
                bool generateNoise = true;


                if (foundPuttingZone) {
                    foreach (var variable in puttingHoleVertices) {
                        if (variable == a) {
                            y = 2f;
                            generateNoise = false;
                        }
                    }
                }
                else {
                    if (i == puttingHoleX && j == puttingHoleZ) {
                        puttingHoleVertices[0] = a + 0;
                        puttingHoleVertices[1] = a + 1;
                        puttingHoleVertices[2] = a + 2;
                        puttingHoleVertices[3] = a + 3;
                        puttingHoleVertices[4] = a + mapXSize + 1;
                        puttingHoleVertices[5] = a + mapXSize + 2;
                        puttingHoleVertices[6] = a + mapXSize + 3;
                        puttingHoleVertices[7] = a + mapXSize + 4;
                        puttingHoleVertices[8] = a + 2 * mapXSize + 2;
                        puttingHoleVertices[9] = a + 2 * mapXSize + 3;
                        puttingHoleVertices[10] = a + 2 * mapXSize + 4;
                        puttingHoleVertices[11] = a + 2 * mapXSize + 5;
                        puttingHoleVertices[12] = a + 3 * mapXSize + 3;
                        puttingHoleVertices[13] = a + 3 * mapXSize + 4;
                        puttingHoleVertices[14] = a + 3 * mapXSize + 5;
                        puttingHoleVertices[15] = a + 3 * mapXSize + 6;

                        y = 2f;
                        generateNoise = false;
                        foundPuttingZone = true;
                    }   
                }

                if (generateNoise) {
                    y = Mathf.PerlinNoise(i * test1, j * test2); 
                }

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
        
        meshCollider.sharedMesh = mesh;
    }

    private void instanciatePrefabs() {
        Vector3 a = transform.TransformPoint(mesh.vertices[puttingHoleVertices[0]]);
        Vector3 b = transform.TransformPoint(mesh.vertices[puttingHoleVertices[15]]);


        Vector3 test = a + (b - a)/2;
        test.y = 2;
        holePosition.position = test;
        Instantiate(holePrefab, holePosition);
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
