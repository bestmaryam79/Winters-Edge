using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectGems : MonoBehaviour
{
    public int counter = 0;
    public TextMeshProUGUI textGems;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        UpdateGemText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This method is called when a gem is collected
    public void CollectedGemFunction()
    {
        counter++;
        UpdateGemText();
        Debug.Log("A gem has been collected. Total gems collected: " + counter);
    }

    // This method updates the text in the UI to show the number of collected gems
    void UpdateGemText()
    {
        textGems.text = "Gems: " + counter;
    }
}