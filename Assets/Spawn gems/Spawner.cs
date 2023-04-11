using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Declaring the variables for item to spawn
    public GameObject itemToSpawn;

    //Number of times I want the item spawn
    public int numSpawns = 5;

    // Declaring public variables for the random spawning position of the item
    public int itemX = 2;
    public int itemY = 2;
    public int itemZ = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Loop to spawn the object
        for (int i = 0; i < numSpawns; i++)
        {
            SpawnItem();
        }
    }

    // SpawnItem method is being used to create a new object clone at a random position within the range
    void SpawnItem()
    {
        // New vector representing the random spawn position
        Vector3 randomPos = new Vector3(Random.Range(-itemX, itemX), Random.Range(-itemY, itemY), Random.Range(-itemZ, itemZ));

        // A new clone of the itemToSpawn GameObject at the random position with no rotation
        GameObject clone = Instantiate(itemToSpawn, randomPos, Quaternion.identity);
    }

}