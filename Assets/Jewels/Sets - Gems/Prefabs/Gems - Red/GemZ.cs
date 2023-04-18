using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemZ : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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
