using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class MapGenerator : MonoBehaviour
{
    [Header("Plane size")]
    [SerializeField] private float sizeX = 100f;
    [SerializeField] private float sizeZ = 100f;

    [Header("Resolution")]
    [SerializeField, Range(10, 200)] private int resolution = 50;

    [Header("Height edit")]
    [SerializeField] private float heightMultiplier = 10f;
    [SerializeField] private float noiseScale = 0.05f;
    [SerializeField] private int seed = 0;

    [Header("Animation")]
    [SerializeField] private bool animate = false;
    [SerializeField] private float animationSpeed = 1f;

    private Mesh mesh;
    private Vector3[] baseVertices;

    void Start()
    {
        GenerateMesh();
    }

    void Update()
    {
        if (animate)
        {
            AnimateMesh();
        }
    }

    private void GenerateMesh()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int vertsX = resolution + 1;
        int vertsZ = resolution + 1;
        int totalVerts = vertsX * vertsZ;

        Vector3[] vertices = new Vector3[totalVerts];
        int[] triangles = new int[(vertsX - 1) * (vertsZ - 1) * 6];
        Vector2[] uv = new Vector2[totalVerts];

        Random.InitState(seed);

        float stepX = sizeX / resolution;
        float stepZ = sizeZ / resolution;

        int triIndex = 0;

        for (int z = 0; z < vertsZ; z++)
        {
            for (int x = 0; x < vertsX; x++)
            {
                int i = z * vertsX + x;

                float posX = x * stepX - sizeX * 0.5f;
                float posZ = z * stepZ - sizeZ * 0.5f;

                float noise = Mathf.PerlinNoise(
                    (posX + seed) * noiseScale,
                    (posZ + seed * 137) * noiseScale
                );

                float height = (noise - 0.5f) * 2f * heightMultiplier;

                vertices[i] = new Vector3(posX, height, posZ);

                uv[i] = new Vector2((float)x / resolution, (float)z / resolution);

                if (x < vertsX - 1 && z < vertsZ - 1)
                {
                    triangles[triIndex++] = i;
                    triangles[triIndex++] = i + vertsX;
                    triangles[triIndex++] = i + 1;

                    triangles[triIndex++] = i + 1;
                    triangles[triIndex++] = i + vertsX;
                    triangles[triIndex++] = i + vertsX + 1;
                }
            }
        }

        baseVertices = (Vector3[])vertices.Clone();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        if (TryGetComponent<MeshCollider>(out var collider))
        {
            collider.sharedMesh = mesh;
        }
    }

    private void AnimateMesh()
    {
        Vector3[] vertices = mesh.vertices;

        float time = Time.time * animationSpeed;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 basePos = baseVertices[i];

            float extraHeight = Mathf.PerlinNoise(
                (basePos.x + time * 0.3f) * noiseScale * 2f,
                (basePos.z + time * 0.4f) * noiseScale * 2f
            ) * 1.5f;

            vertices[i].y = basePos.y + extraHeight;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    private void OnValidate()
    {
        if (Application.isPlaying) return;
        GenerateMesh();
    }
}
