using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public Transform spawnPoint;
    
    private void Awake()
    {
        spawnPoint = transform;
    }
}