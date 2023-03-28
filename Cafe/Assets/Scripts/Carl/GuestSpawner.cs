using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestSpawner : MonoBehaviour
{
    [SerializeField] private Guest guest;
    private GameObject spawnedGuest;
    private float guestSpawnTimer;
    private List<Vector3> spawnPositions = new List<Vector3>();
    
    void Start()
    {
        AddSpawners();
    }
    
    void Update()
    {
        
    }

    private void AddSpawners()
    {
        spawnPositions.Add(FindObjectOfType<GuestSpawnPos>().transform.position);
    }
    
    public void SpawnNewGuest()
    {
        // Vector3 spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Count)];
        Guest spawnedGuest = Instantiate(guest, spawnPositions[Random.Range(0, spawnPositions.Count)], transform.rotation);
        SetupGuest(spawnedGuest);
    }

    private void SetupGuest(Guest spawnedGuest)
    {
        spawnedGuest.OnSpawn(GuestRandomizer());
    }

    private int GuestRandomizer()
    {
        var randomizer = Random.Range(0, 99);
        if (randomizer <= 0 && randomizer >= 49)
            return 1;
        if (randomizer <= 50 && randomizer >= 94)
            return 2;
        return 3;
    }
}


