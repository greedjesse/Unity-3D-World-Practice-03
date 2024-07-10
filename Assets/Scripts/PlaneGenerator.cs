using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class PlaneGenerator : MonoBehaviour
{
    [HideInInspector] public Mesh m;
    private MeshFilter _mf;

    [SerializeField] private Vector2 planeSize = new Vector2(1, 1);
    [SerializeField] private int planeRes = 1;

    private List<Vector3> _verts;
    private List<int> _triangles;
    
    void Start()
    {
        // SetupAndGenerate();
    }

    public void SetupAndGenerate()
    {
        m = new Mesh();
        _mf = GetComponent<MeshFilter>();
        _mf.mesh = m;

        // planeRes = Mathf.Clamp(planeRes, 1, 50); 
        
        GeneratePlane(planeSize, planeRes);
        AssignMesh();
    }

    private void GeneratePlane(Vector2 size, int res)
    {
        _verts = new List<Vector3>();
        float xPerStep = size.x / res;
        float yPerStep = size.y / res;
        for (int y = 0; y < res + 1; y++)
        {
            for (int x = 0; x < res + 1; x++)
            {
                _verts.Add(new Vector3(x * xPerStep, 0, y * yPerStep));
            }
        }

        _triangles = new List<int>();
        for (int r = 0; r < res; r++)
        {
            for (int c = 0; c < res; c++)
            {
                int i = r * (res + 1) + c;
                
                _triangles.Add(i);
                _triangles.Add(i+res+1);
                _triangles.Add(i+res+2);
                
                _triangles.Add(i);
                _triangles.Add(i+res+2);
                _triangles.Add(i+1);
            }
        }
    }

    private void AssignMesh()
    {
        m.Clear();
        m.vertices = _verts.ToArray();
        m.triangles = _triangles.ToArray();
        
        m.RecalculateNormals();
        m.RecalculateTangents();
        m.RecalculateBounds();
    }
}
