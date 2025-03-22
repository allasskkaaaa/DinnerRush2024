using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Object", menuName = "ScriptableObjects/Inventory Object")]
public class InventoryObject : ScriptableObject
{
    public List<FoodObject> inventory = new List<FoodObject>();
}
