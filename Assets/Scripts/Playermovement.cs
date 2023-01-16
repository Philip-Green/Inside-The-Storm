using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : ActorMovement
{
    int whichScene;
    bool distance;
    private void Update()
    {
        // Update mx based on Input.
        mx = Input.GetAxisRaw("Horizontal");

        // Jump check!
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            Jump();
        }

        // Slide check.
        if (Mathf.Abs(mx) > runSpeedThreshold) // Turn whatever value into a positive. 
        {
            if (Input.GetButtonDown("Slide") && isGrounded())
            {
                Slide();
                Debug.Log("Help");
            }
        }
    }

    


} 



