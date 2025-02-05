using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "ScriptableObjects/Food Object")]
public class FoodObject : ScriptableObject
{
    public Sprite thumbnail;
    public string itemName;
    public int quantity;
    public int itemID;
    public List<FoodObject> recipe = new List<FoodObject>();

    public objectTypes type;
    public enum objectTypes
    {
        Ingredient,
        Food
    }
}
