using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;
    [SerializeField] private Transform[] nodes;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float spawnInterval = 20f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InvokeRepeating(nameof(spawnObstacle), 0f, spawnInterval);
    }

    void spawnObstacle()
    {
        int randomNode = Random.Range(0, nodes.Length);
        int randomObstacle = Random.Range(0, obstacles.Length);


        if (nodes[randomNode].childCount == 0) 
        {
            Debug.Log("Obstacle spawned.");
            Instantiate(obstacles[randomObstacle], nodes[randomNode]);
        }
        else
        {
            Debug.Log("There is already an obstacle here.");
        }
    }
}
