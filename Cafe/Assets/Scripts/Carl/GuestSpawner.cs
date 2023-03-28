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
        SpawnNewGuest();
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
        int randomizer = Random.Range(0, 99);
        if (randomizer is >= 0 and <= 49)
            return 1;
        if (randomizer is >= 50 and <= 94)
            return 2;
        return 3;
    }
}


