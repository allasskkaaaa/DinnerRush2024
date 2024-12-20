using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject npc;
    public SpawnNode[] spawnPoints;
    public float spawnInterval;
    private float timer;

    private void Start()
    {
        timer = spawnInterval;
        spawnCustomer();
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
        List<SpawnNode> availableNodes = new List<SpawnNode>();

        // Collect all available spawn points
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!spawnPoints[i].isOccupied) 
            {
                availableNodes.Add(spawnPoints[i]);
            }
        }

        // If there are no available nodes, log a warning and return
        if (availableNodes.Count == 0)
        {
            return;
        }

        // Randomly select one of the available nodes
        SpawnNode randomSpawn = availableNodes[Random.Range(0, availableNodes.Count)];

        // Spawn the NPC and mark the node as occupied
        GameObject spawnedNPC = Instantiate(npc, randomSpawn.transform.position, randomSpawn.transform.rotation);
        randomSpawn.GetComponent<SpawnNode>().isOccupied = true;
        spawnedNPC.GetComponent<Customer>().spawnNode = randomSpawn;

    }

}
