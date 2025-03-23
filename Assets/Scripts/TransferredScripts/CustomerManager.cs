using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public InventoryObject menu;

    public GameObject catPrefab;
    public Cat[] allCats;
    public List<Cat> possibleCats;

    [SerializeField] private float spawnInterval = 5f;
    private float timer;

    [SerializeField] private SpawnNode[] spawnPoints;

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

    public void checkPossibleCats()
    {

        //Iterate through each cat, check their favourite foods and compare each food to each menu item.
        //If the menu item matches, add that cat to the possibleCats list.
        foreach (Cat cat in allCats)
        {
            bool catAlreadyAdded = false;

            foreach (FoodObject food in cat.favouriteFoods)
            {
                for (int i = 0; i < menu.inventory.Count; i++)
                {
                    if (menu.inventory[i] == food)
                    {
                        // Check if the cat is already in the possibleCats list
                        if (!possibleCats.Contains(cat))
                        {
                            possibleCats.Add(cat);
                            cat.appearances++;

                            if (cat.appearances > 3 && !cat.isRegular)
                            {
                                cat.isRegular = true;
                            }
                            
                            catAlreadyAdded = true;
                        }

                        break; // Stop checking more foods for this cat once added
                    }
                }

                // If the cat has been added already, no need to continue checking the remaining foods for this cat
                if (catAlreadyAdded) break;
            }
        }




        Debug.Log("Found customers: " + possibleCats.Count);

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

        // Randomly select a cat
        int randomCat = Random.Range(0, possibleCats.Count);

        // Spawn cat prefab and get the catTemplate component
        GameObject spawnedNPC = Instantiate(catPrefab, randomSpawn.transform.position, randomSpawn.transform.rotation);
        CatTemplate catTemplate = spawnedNPC.GetComponent<CatTemplate>();

        // Set the catTemplate cat to the selected cat from possibleCats
        catTemplate.cat = possibleCats[randomCat];
        catTemplate.spawnNode = randomSpawn;

        // Mark the spawn node as occupied
        randomSpawn.GetComponent<SpawnNode>().isOccupied = true;

        // Initialize the catTemplate
        catTemplate.initCat(catTemplate.cat);  // Ensure this is called after the cat is assigned to the template
    }

}
