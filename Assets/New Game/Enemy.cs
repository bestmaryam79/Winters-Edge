using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject myPrefab;
    public bool further = false;
    public int speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(CalculateDistanceBetweenVectors(player.transform.position, transform.position));

        //look at the player
        transform.LookAt(player.transform.position);

        //grab the distance from between the vectors using our new function
        float distanaceToPlayer = CalculateDistanceBetweenVectors(player.transform.position, transform.position);


        if (distanaceToPlayer > 20f)
        {
            further = true;
        }
        else
        {
            further = false;
        }

        //when the player is far away (5 untis), chase the player
        if (distanaceToPlayer > 5.0f)
        {
            if (further == false)
            {
            //chase the player
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
        else if (distanaceToPlayer < 2.0f)
        {
            

        }
        else
        {
            //hes in the sweep spot
        }



    }


    public float CalculateDistanceBetweenVectors(Vector3 v1, Vector3 v2)
    {
        //square root of 1.x-2.x squared, plus 1.y-2.y squared
        float x = v1.x - v2.x;
        float y = v1.y - v2.y;
        float z = v1.z - v2.z;

        //square root of the x y z squared
        x = Mathf.Pow(x, 2);
        y = Mathf.Pow(y, 2);
        z = Mathf.Pow(z, 2);

        // add the squared values
        float sqXYZ = x + y + z;
        //

        return Mathf.Sqrt(sqXYZ);


    }

    public float CalculateDistanceBetweenVectors(Vector2 v1, Vector2 v2)
    {
        //square root of 1.x-2.x squared, plus 1.y-2.y squared
        float x = v1.x - v2.x;
        float y = v1.y - v2.y;


        //square root of the x y z squared
        x = Mathf.Pow(x, 2);
        y = Mathf.Pow(y, 2);


        // add the squared values
        float sqXY = x + y;
        //

        return Mathf.Sqrt(sqXY);
    }
}
