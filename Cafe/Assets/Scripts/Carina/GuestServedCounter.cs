using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuestServedCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI servedGuestsText;
    [SerializeField] int maxGold;

    public void ServedGuestsCheck(int guests)
    {
        //servedGuestsText.text = string.Format("Served Guests: {0} / {1}", guests, maxServed);
        servedGuestsText.text = string.Format("Gold To Win: {0}", maxGold);
    }

}
