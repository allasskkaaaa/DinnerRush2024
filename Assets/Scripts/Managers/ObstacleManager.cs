using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    [SerializeField] private float minSpawnInterval = 10f;
    [SerializeField] private float maxSpawnInterval = 20f;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();

    [SerializeField] public List<GameObject> spawnedObstacle = new List<GameObject>();

    private float timer;
    private void Start()
    {
        timer += Random.Range(minSpawnInterval, maxSpawnInterval);
    }
    public void spawnObstacle()
    {
        List<Transform> availableNodes = new List<Transform>();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (!spawnPoints[i].GetComponent<Obstacle>().isOccupied)
            {
                availableNodes.Add(spawnPoints[i]);
            }
        }

        if (availableNodes.Count > 0)
        {
            int randomSpawn = Random.Range(0, availableNodes.Count);
            int randomObstacle = Random.Range(0, obstacles.Count);

            GameObject spawnedObject = Instantiate(obstacles[randomObstacle], availableNodes[randomSpawn]);
            availableNodes[randomSpawn].GetComponent<Obstacle>().isOccupied = true;
            spawnedObstacle.Add(spawnedObject);

            if (spawnedObject.CompareTag("Garbage"))
            spawnedObject.GetComponent<GarbagePiece>().spawnNode = availableNodes[randomSpawn].GetComponent<Obstacle>();

        }
        else
        {
            Debug.Log("No place to spawn obstacle");
            return;
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer += Random.Range(minSpawnInterval, maxSpawnInterval);
            spawnObstacle();
        }
        
    }
}
