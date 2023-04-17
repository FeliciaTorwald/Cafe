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
    public EquipTool eT;

    private int queueAmount;
    private float timer = 0f;
    private void Start()
    {
        eT = FindFirstObjectByType<EquipTool>();
    }
    // Make a queue of IEnumerators
    public Queue<IEnumerator> recipeQueue = new Queue<IEnumerator>();

    [SerializeField] private int boba = 0;
    [SerializeField] private int water = 0;
    [SerializeField] TextMeshProUGUI queueText;
    [SerializeField] GameObject finishedTea;
    [SerializeField] GameObject spawnTeaPos;
    [SerializeField] Slider timerSlider;
    GameObject teaToHold;



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

    public void StartMakingTea()
    {
        // Add timer IEnumerator to queue
        recipeQueue.Enqueue(Timer());
        queueAmount++;
        CheckQueueAmount();

        if (!isMakingTea)
        {
            StartCoroutine(Timer());
            canMakeBoba = false;
            //isMakingTea = true;
        }
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
        if (teaToHold == null)
            teaToHold = Instantiate(finishedTea, spawnTeaPos.transform.position, Quaternion.identity) as GameObject;

        isMakingTea = false;
        timerSlider.value = 0;
    }

    public void RemoveBobaTea()
    {
        if (teaToHold != null)
        {
            eT.equipped = false;
            EquipTool.slotIsFull = false;
            Invoke("DestroyBoba", 0.5f);
        }
    }

    public void DestroyBoba()
    {

        Destroy(teaToHold);
    }
    public void CheckQueueAmount()
    {
        queueText.text = queueAmount.ToString();
    }
}
