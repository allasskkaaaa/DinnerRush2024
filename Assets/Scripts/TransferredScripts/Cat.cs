using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cat", menuName = "ScriptableObjects/Cat")]
public class Cat : ScriptableObject
{
    public Sprite thumbnail;
    public string catName;
    public int appearances = 0;
    public bool isRegular;
    public List<FoodObject> favouriteFoods;
}
