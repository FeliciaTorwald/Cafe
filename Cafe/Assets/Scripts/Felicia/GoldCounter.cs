using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldCounter : MonoBehaviour
{
    public TextMeshProUGUI goldCounterText;
    public int amountOfGold;

    public void AddGold(int points)//int adds to gold counter
    {
        amountOfGold += points;
        goldCounterText.text = string.Format("gold " + "{0:0}", amountOfGold);
    }
}
