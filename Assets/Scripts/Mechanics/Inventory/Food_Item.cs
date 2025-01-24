using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "ScriptableObjects/Food")]
public class FoodItem : ScriptableObject
{
    public Sprite thumbnail;
    public string foodName;
    public List<Ingredient> recipe;
}
