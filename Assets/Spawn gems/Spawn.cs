using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    // Declaring an array of GameObjects to spawn from
    public GameObject[] spawnItems;

    // Declare public variables for the number of times to spawn items, as well as the range of random spawn positions
    public int numSpawns = 5;
    public int itemX = 2;
    public int itemY = 2;
    public int itemZ = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Loop to spawn the object "numSpawns" times
        for (int i = 0; i < numSpawns; i++)
        {
            SpawnItem();
        }
    }

    // SpawnItem method is being used to create a new object clone at a random position within the range
    void SpawnItem()
    {
        // Random index to select from the array of spawnItems
        int randomIndex = Random.Range(0, spawnItems.Length);

        // New vector representing the random spawn position
        Vector3 randomPos = new Vector3(Random.Range(-itemX, itemX), Random.Range(-itemY, itemY), Random.Range(-itemZ, itemZ));

        // Creating a new clone of the selected spawnItem at the random position with no rotation
        GameObject clone = Instantiate(spawnItems[randomIndex], randomPos, Quaternion.identity);
    }

}
