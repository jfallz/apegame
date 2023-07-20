using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnsonGenerator : MonoBehaviour
{
    [SerializeField] public GameObject[] objectToSpawn;
    [SerializeField] public float[] heightToSpawn;
    public Vector3 positionToSpawn;
    private List<string> tiles = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        positionToSpawn = new Vector3(7622f, 0f, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++) {
            Transform child = transform.GetChild(i);
            if(child.name.Contains("Tile 1,")) {
                if(!tiles.Contains(child.name)) {
                    tiles.Add(child.name);
                    Transform terrainTile = GameObject.Find(child.name)?.transform.Find("Main Terrain"); 
                    Vector3 centerPosition = GetTerrainCenter(terrainTile.GetComponent<Terrain>());
                    Instantiate(objectToSpawn[0], centerPosition, Quaternion.identity, terrainTile);
                }
            }
        }
    }

    private Vector3 GetTerrainCenter(Terrain terrain)
    {
        // Get the terrain size and resolution
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        int terrainHeightmapWidth = terrain.terrainData.heightmapResolution;
        int terrainHeightmapLength = terrain.terrainData.heightmapResolution;

        // Calculate the center position of the terrain
        Vector3 centerPosition = terrain.transform.position;
        centerPosition.x += terrainWidth / 2.0f;
        centerPosition.z += terrainLength / 2.0f;
        centerPosition.y = terrain.SampleHeight(centerPosition) + terrain.transform.position.y;

        return centerPosition;
    }
}
