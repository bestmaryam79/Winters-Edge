using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    internal enum InputType { keyboard, mobile }
    [SerializeField] private InputType input;    //to display

    [HideInInspector] public float vertical, horizontal, jump;


    // Update is called once per frame
    void Update()
    {
        // Check which input type is selected and perform appropriate input handling
        if (input == InputType.mobile)
        {
            // Add mobile input handling here
        }
        else
        {
            keyboardInput();
        }
    }

    // Handle keyboard input
    void keyboardInput()
    {
        // Get the vertical and horizontal axis values from keyboard input
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        jump = Input.GetAxis("Jump");
    }

}
