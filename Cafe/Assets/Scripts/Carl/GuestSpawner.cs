using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestSpawner : MonoBehaviour
{
    [SerializeField] private List<Guest> guests;
    [SerializeField] private GameManager gameManager;
    private GameObject spawnedGuest;
    private float guestSpawnTimer;
    private List<Vector3> spawnPositions = new();
    private List<Door> doors = new();
    
    void Start()
    {
        StartCoroutine(StartUpFunction());
    }

    void Update()
    {
        
    }

    IEnumerator StartUpFunction()
    {
        yield return new WaitForSeconds(.1f);
        AddSpawners();
        yield return new WaitForSeconds(.1f);
        AddDoors();
        yield return new WaitForSeconds(.1f);
        SpawnNewGuest();
    }

    private void AddSpawners()
    {
        foreach (GuestSpawnPos spawner in FindObjectsOfType<GuestSpawnPos>())
        {
            spawnPositions.Add(spawner.transform.position);
        }
    }
    
    private void AddDoors()
    {
        foreach (Door foundDoor in FindObjectsOfType<Door>())
        {
            doors.Add(foundDoor);
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
        spawnedGuest.GetGuestObject().door = doors[doors.Count-1];
        spawnedGuest.GetGuestObject().gameManager = gameManager;
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


