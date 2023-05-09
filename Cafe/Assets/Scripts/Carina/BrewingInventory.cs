using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BrewingInventory : MonoBehaviour
{
    public bool hasBoba;

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
    public Queue<CraftingRecipe> craftQueue = new Queue<CraftingRecipe>();

    [SerializeField] TextMeshProUGUI queueText;
    [SerializeField] GameObject finishedTea;
    [SerializeField] GameObject spawnTeaPos;
    [SerializeField] Slider timerSlider;
    GameObject teaToHold;


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
            BobaTea(craftQueue);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("BobaPearls"))
    //    {
    //        bSC.Ball.SetActive(false); 
    //    }
    //}

    public void StartMakingTea(CraftingRecipe tea)
    {
        // Add timer IEnumerator to queue
        recipeQueue.Enqueue(Timer());

        // add to queueamount and update queuetext
        queueAmount++;
        CheckQueueAmount();

        if (!isMakingTea)
        {
            StartCoroutine(Timer());

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

        BobaTea(craftQueue);
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
    public void BobaTea(Queue<CraftingRecipe> teaQueue)
    {
        teaToHold = Instantiate(teaQueue.Dequeue().teaToCraft, spawnTeaPos.transform.position, Quaternion.identity) as GameObject;

        teaToHold.GetComponent<Rigidbody>().isKinematic = false;
        teaToHold.transform.position += Random.insideUnitSphere + new Vector3(0, 1.5f, 0);
        Vector3 force = Random.insideUnitSphere * 4;
        force.y = Mathf.Abs(force.y);
        teaToHold.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

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

    public void CheckQueueAmount()
    {
        queueText.text = queueAmount.ToString();
    }
}
