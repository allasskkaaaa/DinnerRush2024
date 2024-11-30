using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NPC : ScriptableObject
{
    public Sprite appearance;
    public Seat seat; //Reference to the seat they are in
    public bool isSeated; //If they are sitting at a seat
    public Vector3 seatLocation; // What location are they sitting at
    public float walkSpeed = 5; //The speed they walk to the table
    public float patience = 15f; //How long they'll wait for food
    public Order currentOrder;
    public mood currentMood;
    public enum Order
    {
        Latte,
        Cake,
        Sandwich
    }

    public enum mood //How much of a tip and how much of a mess they'll leave once finished their meal (if given one)
    {
        Happy,
        Neutral,
        Angry
    }
    private void OnEnable()
    {
        currentOrder = (Order)Random.Range(0, 3);
    }

}
