using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ItemAmount
{
    //public Item item;
    public int amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> materials;
    public List<ItemAmount> results;
}
