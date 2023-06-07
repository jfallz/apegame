using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    [SerializeField] public int numObjects;
    [SerializeField] public float minDistance;
    [SerializeField] public float maxDistance;
    public float currOffset = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Terrain bounds = GetComponent<Terrain>();
        for(int i = 0; i < numObjects; ++i) {
            Vector3 spawnPoint = GetRandomPositionAlongZ(bounds, minDistance, maxDistance);
            if(spawnPoint.z >= bounds.terrainData.size.z)
                continue;
            Instantiate(objectToSpawn, spawnPoint, Quaternion.identity);
        }

    }

    Vector3 GetRandomPositionAlongZ(Terrain terrain, float minDist, float maxDist) {
        float randomDistance = Random.Range(minDist, maxDist);
        currOffset += randomDistance;
        Vector3 spawnPosition = new Vector3(terrain.transform.position.x + 22f, terrain.transform.position.y, terrain.transform.position.z + currOffset);
        return spawnPosition;
    }
}
