using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    //Inventories
    public List<FoodObject> foodInventory = new List<FoodObject>();
    public List<FoodObject> ingInventory = new List<FoodObject>();

    private void Start()
    {
        Instance = this;
    }


}
