using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWave : MonoBehaviour
{
    public MeshFilter meshFilter;

    public int columnCount = 10;
    public float width = 2f;
    public float height = 1f;
    public float k = 0.025f;
    public float m = 1;
    public float drag = 0.025f;
    public float spread = 0.025f;
    public float power = -1f;
    public Color waterColor;

    private List<WaterColumn> columns = new List<WaterColumn>();

    private void Awake()
    {
        Setup();
    }

    private void OnEnable()
    {
        int? column = WorldToColumn(new Vector2(5,0));
        if (column.HasValue)
        {
            columns[column.Value].velocity = power;
        }
    }

    private void Setup()
    {
        columns.Clear();
        float space = width / columnCount;
        for (int i = 0; i < columnCount + 1; i++)
        {
            columns.Add(new WaterColumn(i * space - width * 0.5f, height, k, m, drag));
        }
    }

    internal int? WorldToColumn(Vector2 position)
    {
        float space = width / columnCount;
        int result = Mathf.RoundToInt((position.x + width * 0.5f) / space);
        if (result >= columns.Count || result < 0)
            return null;
        return result;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < columns.Count; i++)
        {
            columns[i].UpdateColumn();
        }

        float[] leftDeltas = new float[columns.Count];
        float[] rightDeltas = new float[columns.Count];
        for (int i = 0; i < columns.Count; i++)
        {
            if (i > 0)
            {
                leftDeltas[i] = (columns[i].height - columns[i - 1].height) * spread;
                columns[i - 1].velocity += leftDeltas[i];
            }
            if (i < columns.Count - 1)
            {
                rightDeltas[i] = (columns[i].height - columns[i + 1].height) * spread;
                columns[i + 1].velocity += rightDeltas[i];
            }
        }

        for (int i = 0; i < columns.Count; i++)
        {
            if (i > 0)
                columns[i - 1].height += leftDeltas[i];
            if (i < columns.Count - 1)
                columns[i + 1].height += rightDeltas[i];
        }

        Mesh mesh = new Mesh();
        Vector3[] verticles = new Vector3[columns.Count * 2];
        int v = 0;
        for (int i = 0; i < columns.Count; i++)
        {
            verticles[v] = new Vector2(columns[i].xPos, columns[i].height);
            verticles[v + 1] = new Vector2(columns[i].xPos, 0f);

            v += 2;
        }

        int[] traingles = new int[(columns.Count - 1) * 6];
        int t = 0;
        v = 0;
        for (int i = 0; i < columns.Count - 1; i++)
        {
            traingles[t] = v;
            traingles[t + 1] = v + 2;
            traingles[t + 2] = v + 1;
            traingles[t + 3] = v + 1;
            traingles[t + 4] = v + 2;
            traingles[t + 5] = v + 3;

            v += 2;
            t += 6;
        }

        mesh.vertices = verticles;
        mesh.triangles = traingles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();

        meshFilter.mesh = mesh;

        Color[] colors = new Color[mesh.vertices.Length];

        for (int i = 0; i < mesh.vertices.Length; i++)
            colors[i] = waterColor;

        meshFilter.mesh.colors = colors;
    }

    
    public class WaterColumn
    {
        public float xPos, height, targetHeight, k, m, velocity, drag;

        public WaterColumn(float xPos, float targetHeight, float k, float m, float drag)
        {
            this.xPos = xPos;
            this.targetHeight = targetHeight;
            this.k = k;
            this.m = m;
            this.drag = drag;
        }

        public void UpdateColumn()
        {
            float a = -k / m * (height - targetHeight);
            velocity += a;
            velocity -= drag * velocity;
            height += velocity;
        }
    }
}
