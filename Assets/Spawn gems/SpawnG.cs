using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnG : MonoBehaviour
{
    // Declaring public variables for the object to spawn and number of times to spawn it
    public GameObject itemToSpawn;
    public int numSpawns = 7;
    public int numGemsCollected = 0;

    public GameObject gemPrefab;

    public GameObject player;

    // Declaring variables for the area in which to spawn objects
    private Vector3 terrainPosition;
    private Vector3 terrainSize;

    // Rotation speed of the spawned object
    public float rotationSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        numGemsCollected = 0;
        // Get the active terrain's position and size
        Terrain terrain = Terrain.activeTerrain;
        terrainPosition = terrain.transform.position;
        terrainSize = terrain.terrainData.size;

        player = GameObject.FindGameObjectWithTag("Player");

        // Loop to spawn the object "numSpawns" times
        for (int i = 0; i < numSpawns; i++)
        {
            SpawnItem();
        }
    }

    // This method is called in the Unity Editor when the user selects the GameObject with this script attached and is used to draw a Gizmo in the scene view to represent the area in which objects will be spawned
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f); // Set the colour of the Gizmo to red with a 50% opacity
        Gizmos.DrawCube(terrainPosition + terrainSize / 2, terrainSize); // Draw a cube with the dimensions of "terrainSize" centered at "terrainPosition" to represent the spawning area
    }

    // This method spawns a single object at a random position within the defined spawning area
    void SpawnItem()
    {
        // Calculate a random position within the terrain
        Vector3 position = terrainPosition + new Vector3(Random.Range(0, terrainSize.x), 0, Random.Range(0, terrainSize.z));

        // Get the height at the position to make sure the object is spawned on the terrain
        position.y = Terrain.activeTerrain.SampleHeight(position);

        // Instantiate the object at the random position with no rotation
        GameObject item = Instantiate(itemToSpawn, position, Quaternion.identity);

        // Rotate the object around its up axis (y-axis) at the speed specified by the rotationSpeed variable
        item.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void SpawnGem()
    {
        // Instantiate the gem prefab
        GameObject newGem = Instantiate(gemPrefab, transform.position, Quaternion.identity);

        // Add a box collider component to the gem object
        BoxCollider collider = newGem.AddComponent<BoxCollider>();

        // Set the size of the box collider to match the size of the gem object
        collider.size = newGem.GetComponent<Renderer>().bounds.size;

        newGem.tag = "Gem";
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            Destroy(other.gameObject);

            numGemsCollected++;

            // Update UI or other relevant code to display the new gem count...
        }
    
    }
    */

    private void OnTriggerEnter(Collider other)
    {



        switch (other.gameObject.tag)
        {
            
            case "Player":
            Debug.Log(other.name);
            CollectGems playercollectscript = other.gameObject.GetComponent<CollectGems>();
            playercollectscript.collectedGems = true;
            playercollectscript.CollectedGemFunction();
            Destroy(gameObject);
                break;

        }

    }



}