using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TerrainMesh : MonoBehaviour
{
    public Terrain terrain;
    public GameObject targetObject;

    private PlaneGenerator _pg;
    void Start()
    {
        _pg = GetComponent<PlaneGenerator>();
        
        _pg.SetupAndGenerate();
        CopyTerrainMesh();
        
        AssetDatabase.CreateAsset(_pg.m, "Assets//test.asset");
        AssetDatabase.SaveAssets();
    }

    private void CopyTerrainMesh()
    {
        var mf = targetObject.GetComponent<MeshFilter>();
        var m = mf.mesh;
        List<Vector3> newVerts = new List<Vector3>();

        foreach (var vert in m.vertices)
        {
            // var worldPos = targetObject.transform.localToWorldMatrix * vert + new Vector4(bounds.extents.x, 0, bounds.extents.z, 0);
            var worldPos = targetObject.transform.localToWorldMatrix * vert;
            var newVert = vert;
            newVert.y = terrain.SampleHeight(worldPos);
            newVerts.Add(newVert);
        }
        
        m.SetVertices(newVerts.ToArray());
        
        m.RecalculateNormals();
        m.RecalculateTangents();
        m.RecalculateBounds();
    }
}
