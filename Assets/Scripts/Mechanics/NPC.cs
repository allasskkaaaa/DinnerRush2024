using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NPC : ScriptableObject
{
    public Sprite appearance;
    public bool isSeated; //If they are sitting at a seat
    public Vector3 seatLocation; // What location are they sitting at
    public int walkSpeed; //The speed they walk to the table
    public float patience; //How long they'll wait for food
    public Order currentOrder;
    public mood currentMood;

    public enum mood //How much of a tip and how much of a mess they'll leave once finished their meal (if given one)
    {
        Happy,
        Neutral,
        Angry
    }

    public enum Order
    {
        Latte,
        Cake,
        Sandwich
    }

}
