﻿using System;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {
    private Mesh mesh;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private GameObject holePrefab;
    [SerializeField] private Transform holePosition;
    [SerializeField] private GameObject playerStartPosition;
    [SerializeField] private GameObject poplarTreePrefab;
    [SerializeField] private GameObject firTreePrefab;
    [SerializeField] private GameObject flagMarker;

    private SeedManager _seedManager;
    
    private Vector3[] vertices;
    private int[] triangles;

    public int mapXSize = 2;
    public int mapZSize = 2;

    private bool foundPuttingZone = false;
    private bool foundStartZone = false;
    private int[] puttingHoleVertices = new int[16];
    private int[] startZoneVertices = new int[16];
    
    // TODO : Put random values here
    private float perlinX;
    private float perlinZ;
    private int puttingHoleX;
    private int puttingHoleZ;
    

    // Start is called before the first frame update
    void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        _seedManager = SeedManager.instance;
        setRandomValues(_seedManager.getSeed());
        createShape();
        updateMesh();
        instanciatePrefabs();
    }

    void createShape() {
        vertices = new Vector3[(mapXSize + 1) * (mapZSize + 1)];

        int a = 0;

        int startZoneX = 0;
        int startZoneZ = 0;    

        // TODO : Choisir la position du point de départ
        if (puttingHoleX < mapXSize/2 && puttingHoleZ < mapZSize/2) {
            startZoneX = mapXSize - 4;
            startZoneZ = mapZSize - 4;
        } else if (puttingHoleX >= mapXSize/2 && puttingHoleZ < mapZSize/2) {
            startZoneX = 0;
            startZoneZ = mapZSize - 4;
        } else if (puttingHoleX >= mapXSize/2 && puttingHoleZ >= mapZSize/2) {
            startZoneX = startZoneZ = 0;
        } else if (puttingHoleX < mapXSize/2 && puttingHoleZ >= mapZSize/2) {
            startZoneX = mapXSize - 4;
            startZoneZ = 0;
        }
        
        
        
        for (int j = 0; j <= mapXSize; j++) {
            for (int i = 0; i <= mapZSize; i++) {
                float y = 0;
                bool generateNoise = true;


                if (foundPuttingZone) {
                    foreach (var variable in puttingHoleVertices) {
                        if (variable == a) {
                            y = 0.7f;
                            generateNoise = false;
                        }
                    }
                }
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

                    y = 0.7f;
                    generateNoise = false;
                    foundPuttingZone = true;
                }

                if (foundStartZone) {
                    foreach (var variable in startZoneVertices) {
                        if (variable == a) {
                            y = 0.7f;
                            generateNoise = false;
                        }
                    }
                }
                
                if (i == startZoneX && j == startZoneZ) {
                    startZoneVertices[0] = a + 0;
                    startZoneVertices[1] = a + 1;
                    startZoneVertices[2] = a + 2;
                    startZoneVertices[3] = a + 3;
                    startZoneVertices[4] = a + mapXSize + 1;
                    startZoneVertices[5] = a + mapXSize + 2;
                    startZoneVertices[6] = a + mapXSize + 3;
                    startZoneVertices[7] = a + mapXSize + 4;
                    startZoneVertices[8] = a + 2 * mapXSize + 2;
                    startZoneVertices[9] = a + 2 * mapXSize + 3;
                    startZoneVertices[10] = a + 2 * mapXSize + 4;
                    startZoneVertices[11] = a + 2 * mapXSize + 5;
                    startZoneVertices[12] = a + 3 * mapXSize + 3;
                    startZoneVertices[13] = a + 3 * mapXSize + 4;
                    startZoneVertices[14] = a + 3 * mapXSize + 5;
                    startZoneVertices[15] = a + 3 * mapXSize + 6;
                    
                    y = 0.7f;
                    generateNoise = false;
                    foundStartZone = true;
                }
 


                if (generateNoise) {
                    y = Mathf.PerlinNoise(i * perlinX, j * perlinZ); 
                }

                vertices[a] = new Vector3(i, y, j);
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
        
        Vector3 puttingPosition = a + (b - a)/2;
        puttingPosition.y = 0.7f;
        holePosition.position = puttingPosition;
        holePosition.localScale = new Vector3(0.07f, 0.07f, 0.07f);
        holePrefab.transform.position = holePosition.position;
        holePrefab.transform.localScale = holePosition.localScale;

        Vector3 flagPosition = holePosition.position;
        flagPosition.y = 4f;
        
        flagMarker.transform.position = flagPosition;      
        
        
        Vector3 c = transform.TransformPoint(mesh.vertices[startZoneVertices[0]]);
        Vector3 d = transform.TransformPoint(mesh.vertices[startZoneVertices[15]]);
        
        Vector3 startPosition = c + (d - c) / 2;
        startPosition.y = 0.7f;
        playerStartPosition.transform.position = startPosition;

        // Instanciate trees
        for (int i = 0; i < Random.Range(6, 15); i++) {
            bool isInUsedZone = false;
            int position = Random.Range(0, vertices.Length);
            
            foreach (var variable in puttingHoleVertices) {
                if (position == variable) {
                    isInUsedZone = true;
                }
            }
            foreach (var variable in startZoneVertices) {
                if (position == variable) {
                    isInUsedZone = true;
                }
            }

            if (!isInUsedZone) {
                float treeType = Random.Range(0, 2);
                if (treeType == 0f) {
                    var test = new GameObject();
                    test.transform.position = transform.TransformPoint(mesh.vertices[position]);
                    Instantiate(poplarTreePrefab, test.transform);
                }
                else {
                    var test = new GameObject();
                    test.transform.position = transform.TransformPoint(mesh.vertices[position]);
                    Instantiate(firTreePrefab, test.transform);
                }
            }
        }
        
    }

    private void setRandomValues(String seed) {
        if (string.IsNullOrEmpty(seed)) {
            perlinX = Random.Range(1, 9) / 10f;
            perlinZ = Random.Range(1, 4) / 10f;

            puttingHoleX = Random.Range(0, mapXSize - 4);
            puttingHoleZ = Random.Range(0, mapZSize - 4);
        } else {
            // TODO : on va absolument devoir faire un "switcheroo" pour mettre les valeurs sur la même base que ceux générés
            for (int i = 0; i < seed.Length; i++) {
                if (i == 0) {
                    perlinX = Mathf.InverseLerp(1, 9, (float) Char.GetNumericValue(seed[i])) / 10f;
                }
                if (i == 1) {
                    perlinZ = Mathf.InverseLerp(1, 4, (float) Char.GetNumericValue(seed[i])) / 10f;
                }

                if (i == 2) {
                    puttingHoleX = (int) Mathf.InverseLerp(0, mapXSize - 4, (float) Char.GetNumericValue(seed[i])) / 10;
                }

                if (i == 3) {
                    puttingHoleZ = (int) Mathf.InverseLerp(0, mapZSize - 4, (float) Char.GetNumericValue(seed[i])) / 10;
                }
            }
        }
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
