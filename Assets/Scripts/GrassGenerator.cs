using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GrassGenerator : MonoBehaviour
{
    [Header("Terrain")]
    [SerializeField] private Terrain terrain;
    [SerializeField] private Mesh terrainMesh;
    [SerializeField] private Vector3 start;
    
    [Header("Grass")]
    [SerializeField] private List<GameObject> grasses;
    [SerializeField] private int density;
    [Tooltip("The height offset")] [SerializeField] private float yOffset;
    [SerializeField] private int xzOffset;
    
    private Bounds _bounds;
    
    void Start()
    {
        GenerateGrass();
    }

    private void GenerateGrass()
    {
        _bounds = terrain.terrainData.bounds;
        float startHeight = start.y + _bounds.size.y + 1;
        float xPerStep = _bounds.size.x / density;
        float zPerStep = _bounds.size.z / density;

        for (int x = 0; x < density ; x++)
        {
            for (int z = 0; z < density; z++)
            {
                float currX = start.x + xPerStep * x + Random.Range(-xzOffset, xzOffset) / 10.0f;
                float currY = startHeight;
                float currZ = start.z + zPerStep * z + Random.Range(-xzOffset, xzOffset) / 10.0f;
                
                Physics.Raycast(new Vector3(currX, startHeight, currZ), new Vector3(0, -90, 0), out RaycastHit hit, Mathf.Infinity, ~ 1 << 3);
                currY -= hit.distance - yOffset;

                if (currX > start.x && currX < start.x + _bounds.size.x && 
                    currZ > start.z && currZ < start.z + _bounds.size.z)
                {
                    Instantiate(grasses[0], new Vector3(currX, currY, currZ), Quaternion.Euler(0, 0, 0));    
                }
            }
        }
    }
}
