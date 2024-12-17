using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject npc;
    public Transform[] spawnPoints;
    public float spawnInterval;
    private float timer;

    private void Start()
    {
        timer = spawnInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            spawnCustomer();
            timer = spawnInterval;
        }


    }
    private void spawnCustomer()
    {
        List<Transform> availableNodes = new List<Transform>();

        // Collect all available spawn points
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (!spawnPoint.GetComponent<SpawnNode>().isOccupied)
            {
                availableNodes.Add(spawnPoint);
            }
        }

        // If there are no available nodes, log a warning and return
        if (availableNodes.Count == 0)
        {
            Debug.LogWarning("No available spawn nodes.");
            return;
        }

        // Randomly select one of the available nodes
        Transform randomSpawn = availableNodes[Random.Range(0, availableNodes.Count)];

        // Spawn the NPC and mark the node as occupied
        Instantiate(npc, randomSpawn.position, randomSpawn.rotation);
        randomSpawn.GetComponent<SpawnNode>().isOccupied = true;

        Debug.Log("NPC Spawned at " + randomSpawn.name);
    }

}
