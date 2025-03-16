using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public Inventory menu;

    public GameObject catPrefab;
    public Cat[] allCats;
    public List<Cat> possibleCats;

    [SerializeField] private float spawnInterval = 5f;
    private float timer;

    [SerializeField] private SpawnNode[] spawnPoints;

    private void Start()
    {
        timer = spawnInterval;

        checkPossibleCats();
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

    private void checkPossibleCats()
    {
        //Iterate through each cat, check their favourite foods and compare each food to each menu item.
        //If the menu item matches, add that cat to the possibleCats list.
        foreach (Cat cat in allCats)
        {
            foreach (FoodObject dish in cat.favouriteFoods)
            {
                for (int i = 0; i < menu.list.Count; i++)
                {
                   if (menu.list[i] == dish)
                    {
                        possibleCats.Add(cat);
                    }
                }
            }
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


        //Randomly select a cat
        int randomCat = Random.Range(0, possibleCats.Count);

        // Spawn cat prefab and set the catTemplate cat as the randomly selected cat from possibleCats
        GameObject spawnedNPC = Instantiate(catPrefab, randomSpawn.transform.position, randomSpawn.transform.rotation);
        CatTemplate catTemplate = catPrefab.GetComponent<CatTemplate>();
        catTemplate.cat = possibleCats[randomCat];
        randomSpawn.GetComponent<SpawnNode>().isOccupied = true;
        spawnedNPC.GetComponent<Customer>().spawnNode = randomSpawn;


    }
}
