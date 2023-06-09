using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Declaring public variables for the object to spawn and number of times to spawn it
    public GameObject itemToSpawn;
    public int numSpawns = 7;

    // Declaring variables for the area in which to spawn objects
    private Vector3 terrainPosition;
    private Vector3 terrainSize;

    // Start is called before the first frame update
    void Start()
    {
        // Get the active terrain's position and size
        Terrain terrain = Terrain.activeTerrain;
        terrainPosition = terrain.transform.position;
        terrainSize = terrain.terrainData.size;
        // Loop to spawn the object "numSpawns" times
        for (int i = 0; i < numSpawns; i++)
        {
            SpawnEnemy();
        }
    }

    // This method is called in the Unity Editor when the user selects the GameObject with this script attached and is used to draw a Gizmo in the scene view to represent the area in which objects will be spawned
   /* void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f); // Set the colour of the Gizmo to red with a 50% opacity
        Gizmos.DrawCube(centre, size); // Draw a cube with the dimensions of "size" centered at "centre" to represent the spawning area
    }
   */
    // This method spawns a single object at a random position within the defined spawning area
    void SpawnEnemy()
    {
        // Calculate a random position within the terrain
        Vector3 position = terrainPosition + new Vector3(Random.Range(0, terrainSize.x), 0, Random.Range(0, terrainSize.z));

        // Get the height at the position to make sure the object is spawned on the terrain
        position.y = Terrain.activeTerrain.SampleHeight(position);

        // Instantiate the object at the random position with no rotation
        GameObject item = Instantiate(itemToSpawn, position, Quaternion.identity);
    }





}

