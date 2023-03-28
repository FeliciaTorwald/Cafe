using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestSpawner : MonoBehaviour
{
    [SerializeField] private List<Guest> guests;
    private GameObject spawnedGuest;
    private float guestSpawnTimer;
    private List<Vector3> spawnPositions = new();
    
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
        foreach (GuestSpawnPos spawner in FindObjectsOfType<GuestSpawnPos>())
        {
            spawnPositions.Add(spawner.transform.position);
        }
    }
    
    public void SpawnNewGuest()
    {
        int guestRandomizerResult = GuestRandomizer();

        Guest spawnedGuest = Instantiate(guests[guestRandomizerResult], spawnPositions[Random.Range(0, spawnPositions.Count)], transform.rotation);

        SetupGuest(spawnedGuest, guestRandomizerResult);
    }

    private void SetupGuest(IGuest spawnedGuest, int RandomizerResult)
    {
        spawnedGuest.OnSpawn();
    }

    private int GuestRandomizer()
    {
        int randomizer = Random.Range(0, 99);
        if (randomizer is >= 0 and <= 49)
            return 0;
        if (randomizer is >= 50 and <= 94)
            return 1;
        return 2;
    }
}


