using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NPC;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float originalSpeed;
    [SerializeField] private float slowSpeed = 2;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name + " is walking through an obstacle.");
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                originalSpeed = playerController.moveSpeed;
                playerController.moveSpeed = slowSpeed;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.name + " is leaving an obstacle.");
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.moveSpeed = originalSpeed;
            }

        }
       
    }
}
