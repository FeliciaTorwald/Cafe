using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestSpawner : MonoBehaviour
{
    [SerializeField] private List<Guest> guests;
    private GameObject spawnedGuest;
    private float guestSpawnTimer;
    public List<Vector3> spawnPositions = new();
    private List<Door> doors = new();
    
    void Start()
    {
        StartCoroutine(StartUpFunction());
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
        
        //Adds the guest spawner objects as use when spawning new guests.
    }
    
    private void AddDoors()
    {
        foreach (Door foundDoor in FindObjectsOfType<Door>())
        {
            doors.Add(foundDoor);
        }
        
        //Adds the doors in the scene for reference by the guests. Can be removed if we decide to only
        //use a single door in the scene.
    }
    
    public void SpawnNewGuest()
    {
        int guestRandomizerResult = GuestRandomizer();
        SetupGuest(guestRandomizerResult);
        
        //Spawns a new guest of random type.
    }

    private void SetupGuest(int RandomizerResult)
    {
        Guest spawnedGuest = Instantiate(guests[RandomizerResult], spawnPositions[Random.Range(0, spawnPositions.Count)], transform.rotation);
        spawnedGuest.door = doors[doors.Count-1];
        GameManager.Instance.guestsInScene.Add(spawnedGuest);
        
        //Setup the guest, here we seed the type of guest, what tea they want etc.
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


