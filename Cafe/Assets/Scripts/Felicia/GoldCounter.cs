using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldCounter : MonoBehaviour
{
    public TextMeshProUGUI bobaCounterText;
    public int amountOfGold;



    public void AddBoba(int points)//int adds to boba counter
    {
        amountOfGold += points;
        bobaCounterText.text = string.Format("boba " + "{0:0}", amountOfGold);

    }
}
