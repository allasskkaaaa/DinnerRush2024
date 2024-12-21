using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private SeatManager seatManager;
    [SerializeField] private float spawnInterval = 10;
    [SerializeField] private Transform spawnPoint;
    private float cooldownTimer;

    private void Update()
    {
        // Timer countdown
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            cooldownTimer = spawnInterval;

            // Check if there are available seats
            var availability = seatManager.CheckAvailability();
            if (!availability.Item1) // If there are available seats (Item1 is false)
            {
                Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);
                Debug.Log("NPC spawned!");
            }
            else
            {
                Debug.Log("No available seats. NPC not spawned.");
            }
        }
    }

}
