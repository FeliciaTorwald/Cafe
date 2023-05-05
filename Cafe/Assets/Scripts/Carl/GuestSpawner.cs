using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestSpawner : MonoBehaviour
{
    [SerializeField] private List<Guest> guests;
    private GameObject spawnedGuest;
    private float guestSpawnTimer = 2f;
    public List<Transform> spawnPositions = new();
    private List<Door> doors = new();
    private int maxNumberOfGuests;

    void Start()
    {
        StartCoroutine(StartUpFunction());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            SpawnNewGuest();
        }
    }

    IEnumerator StartUpFunction()
    {
        yield return new WaitForSeconds(.1f);
        AddSpawners();
        yield return new WaitForSeconds(.1f);
        AddDoors();
        yield return new WaitForSeconds(.5f);
        maxNumberOfGuests = GameManager.Instance.SetNumberOfGuests();
        yield return new WaitForSeconds(.2f);
        SpawnNewGuest();
        
    }

    private void AddSpawners()
    {
        foreach (GuestSpawnPos spawner in FindObjectsOfType<GuestSpawnPos>())
        {
            spawnPositions.Add(spawner.transform);
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
        if (guestSpawnTimer <= 0 && GameManager.Instance.guestsInScene.Count < maxNumberOfGuests)
        {
            int guestRandomizerResult = GuestRandomizer();
            SetupGuest(guestRandomizerResult);
            guestSpawnTimer = Random.Range(5f, 8f);
            
        }
        else
        {
             guestSpawnTimer -= 1 * Time.deltaTime;
        }
        //Spawns a new guest of random type.
    }

    private void SetupGuest(int RandomizerResult)
    {
        Transform spawnPos = spawnPositions[Random.Range(0, spawnPositions.Count)];
        Guest spawnedGuest = Instantiate(guests[RandomizerResult], spawnPos.position, transform.rotation);
        spawnedGuest.door = doors[doors.Count-1];
        spawnedGuest.guestSpawnPos = spawnPos;
        spawnedGuest.guestSpawner = this;
        GameManager.Instance.AddGuest(spawnedGuest);

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


