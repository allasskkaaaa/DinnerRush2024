using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cat", menuName = "ScriptableObjects/Cat")]
public class Cat : ScriptableObject
{
    public Sprite thumbnail;
    public string catName;
    public List<FoodItem> favouriteFoods;
}
