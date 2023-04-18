using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectGems : MonoBehaviour
{
    public bool collectedGems;
    int counter;
    public TextMeshProUGUI textGems;

    public CollectGems(bool collectedGems)
    {
        this.collectedGems = collectedGems;
    }




    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This method is called when a gem is collected
    public void CollectedGemFunction()
    {
        counter++;
        textGems.text = ("Gems" + counter.ToString());
        Debug.Log("The collected gem function has just been called " + counter);
    }
}