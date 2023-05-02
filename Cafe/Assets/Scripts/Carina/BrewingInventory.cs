using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BrewingInventory : MonoBehaviour
{
    public bool hasBoba;
    public bool hasWater;
    public bool canMakeBoba = true;
    public bool isMakingTea;
    public float gameTime = 10f;
    public BobaShooterController bSC;
    public ParticleSystem boiling;
    public ParticleSystem leftFire;
    public ParticleSystem rightFire;
    public AudioSource source;
    public AudioClip boilingSound;

    private int queueAmount;
    private float timer = 0f;

    // Make a queue of IEnumerators
    public Queue<IEnumerator> recipeQueue = new Queue<IEnumerator>();

    [SerializeField] private int water = 0;
    [SerializeField] TextMeshProUGUI queueText;
    [SerializeField] GameObject finishedTea;
    [SerializeField] GameObject spawnTeaPos;
    [SerializeField] Slider timerSlider;
    GameObject teaToHold;
    List<GameObject> finTeaList = new List<GameObject>();

    private void Start()
    {
        boiling.Pause();
        rightFire.Pause();
        leftFire.Pause();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            BobaTea();
        }
    }

    // On collision, will check if player has boba, and if they do, add boba to count
    private void OnTriggerEnter(Collider collision)
    {
     
        // If player has water it will add water to it
        if (collision.gameObject.CompareTag("Player") && hasWater)
        {
            water++;
            hasWater = false;
            FindObjectOfType<Get_water_In_Teapot>().PouringWater();
        }

    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("BobaPearls"))
    //    {
    //        bSC.Ball.SetActive(false); 
    //    }
    //}

    public void StartMakingTea()
    {
        // Add timer IEnumerator to queue
        recipeQueue.Enqueue(Timer());

        // add to queueamount and update queuetext
        queueAmount++;
        CheckQueueAmount();

        if (!isMakingTea)
        {
            StartCoroutine(Timer());
            canMakeBoba = false;
            //isMakingTea = true;
        }
        boiling.Play();
        source.PlayOneShot(boilingSound);
        rightFire.Play();
        leftFire.Play();
    }

    // When called will wait 10sec before calling function Bobatea, while filling the slider to show progress remaining
    private IEnumerator Timer()
    {

        if (recipeQueue.Count == 0)
        {
            yield break;
        }

        // dequeue oldest ienumerator from queue
        IEnumerator currentRecipe = recipeQueue.Dequeue();

        timer = gameTime;

        do
        {
            timer -= Time.deltaTime;
            timerSlider.value = 1 - timer / gameTime;

            yield return null;

        } while (timer > 0);

        BobaTea();
        queueAmount--;
        CheckQueueAmount();

        // if queue is over 0, start timer coroutine 
        if (recipeQueue.Count > 0)
        {
            yield return StartCoroutine(Timer());
        }
        else
        {
            queueText.text = "";
        }
    }

    // Spawns finished tea at a spawnpoint set to pot position
    public void BobaTea()
    {
        teaToHold = Instantiate(finishedTea, spawnTeaPos.transform.position, Quaternion.identity) as GameObject;

        teaToHold.GetComponent<Rigidbody>().isKinematic = false;
        teaToHold.transform.position += Random.insideUnitSphere + new Vector3(0, 1.5f, 0);
        Vector3 force = Random.insideUnitSphere * 4;
        force.y = Mathf.Abs(force.y);
        teaToHold.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

        finTeaList.Add(teaToHold);
        isMakingTea = false;
        timerSlider.value = 0;
    }

    public void RemoveBobaTea(GameObject teaToRemove)
    {
        //if (teaToHold != null)
        {
            EquipTool.slotIsFull = false;
            //Invoke("DestroyBoba", 0.1f);
            Destroy(teaToRemove, 0.1f);
        }
    }

    public void DestroyBoba()
    {
        Destroy(teaToHold);
        //Destroy(finTeaList[0]);
        
        //finTeaList.RemoveAt(0);
    }
    public void CheckQueueAmount()
    {
        queueText.text = queueAmount.ToString();
    }
}
