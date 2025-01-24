using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "ScriptableObjects/Ingredient")]
public class Ingredient : ScriptableObject
{
    public Sprite thumbnail;
    public string itemName;
    public int quantity;
    public int itemID;
}
