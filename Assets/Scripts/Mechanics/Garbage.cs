using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Dish food = collision.GetComponent<Dish>();

        if (food != null || collision.CompareTag("Obstacle"))
        {
            Debug.Log("Throwing out food");
            Destroy(collision.gameObject);
        }
    }
}
