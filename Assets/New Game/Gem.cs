using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("something has entered the trigger, and it was " + other.gameObject.name);

        if (other.gameObject.name == "Gem")
        {
            Debug.Log("player has entered the trigger, the gem is collected");
            Destroy(gameObject, 0.5f);

            //add a gem to the player
           // other.gameObject.GetComponent<Player>().CollectedCoin(1);

        }

    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("still inside!!!");
    }
}
