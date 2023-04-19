using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemZ : MonoBehaviour
{
    //public GameObject gemPrefab;
    public void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Gem")
        {
            Debug.Log("Gem connected");
            Destroy(gameObject);
        }
    }
}
