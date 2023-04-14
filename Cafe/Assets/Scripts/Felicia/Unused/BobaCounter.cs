using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BobaCounter : MonoBehaviour
{
    public TextMeshProUGUI bobaCounterText;
    public int amountOfBoba;
    


    public void AddBoba(int points)//int adds to boba counter
    {
        amountOfBoba += points;
        bobaCounterText.text = string.Format("boba " + "{0:0}", amountOfBoba);

    }
  
}

