using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Resource,
    Product,
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Info")]
    public string displayName;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;
}
