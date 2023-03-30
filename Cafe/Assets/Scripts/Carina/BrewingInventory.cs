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
    public float gameTime;

    private bool stopTimer;

    [SerializeField] private int boba = 0;
    [SerializeField] private int water = 0;

    [SerializeField] GameObject finishedTea;
    [SerializeField] GameObject spawnTeaPos;
    [SerializeField] TextMeshProUGUI addItemText;
    [SerializeField] Slider timerSlider;
    [SerializeField] TextMeshProUGUI timerText;


    private void Start()
    {
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        //timerSlider.value
    }

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

        if (collision.gameObject.CompareTag("Player") && hasWater)
        {
            water++;
            hasWater = false;
        }

        if (boba >= 2 && canMakeBoba && water >= 1)
        {
            BobaTea();
            canMakeBoba = false;
        }
    }

    private void BobaTea()
    {
        Instantiate(finishedTea, spawnTeaPos.transform.position, Quaternion.identity);
        boba = 0;
        water = 0;
    }
}
