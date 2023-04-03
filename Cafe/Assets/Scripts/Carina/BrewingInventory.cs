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
    public float gameTime = 10f;

    private float timer = 0f;

    [SerializeField] private int boba = 0;
    [SerializeField] private int water = 0;

    [SerializeField] GameObject finishedTea;
    [SerializeField] GameObject spawnTeaPos;
    [SerializeField] TextMeshProUGUI addItemText;
    [SerializeField] Slider timerSlider;

    GameObject teaToHold;


    // On collision, will check if player has boba, and if they do, add boba to count
    private void OnCollisionEnter(Collision collision)
    {
        if (hasBoba)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                boba++;
                hasBoba = false;
            }
        }

        // If player has water it will add water to it
        if (collision.gameObject.CompareTag("Player") && hasWater)
        {
            water++;
            hasWater = false;
        }

        // If amount of boba is equal or more than 2, and have water it will start the timer that will make boba tea
        if (boba >= 2 && canMakeBoba && water >= 1)
        {
            StartCoroutine(Timer());
            canMakeBoba = false;
        }
    }

    // When called will wait 10sec before calling function Bobatea, while filling the slider to show progress remaining
    private IEnumerator Timer()
    {
        timer = gameTime;

        do
        {
            timer -= Time.deltaTime;
            timerSlider.value = 1 - timer / gameTime;

            yield return null;

        } while (timer > 0);

        BobaTea();
    }

    // Spawns finished tea at a spawnpoint set to pot position
    public void BobaTea()
    {
        if (teaToHold == null)
            teaToHold = Instantiate(finishedTea, spawnTeaPos.transform.position, Quaternion.identity) as GameObject;
        boba = 0;
        water = 0;
    }

    public void RemoveBobaTea()
    {
        if (teaToHold != null)
        {
            Destroy(teaToHold,0.5f);
        }
    }


}
