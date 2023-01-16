using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorMovement : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce = 20f;
    public Rigidbody2D rb;
    public Transform feet;
    public LayerMask groundLayers;
    public Animator anim;

    // how much movement counts as "running"!
    protected float runSpeedThreshold = 0.05f;
    
    // how much a slide multiplies your movement?
    protected float slideSpeedMultiplier = 2.0f;

    // x velocity!
    protected float mx;

    // facing!
    protected bool isFacingRight;


    protected void FixedUpdate()
    {
        // Orient left or right based on Horizontal input.
        if (mx > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (mx < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }


        // Ground check.
        anim.SetBool("isGrounded", isGrounded());

        // Speed bonus for sliding - check the animator for this!
        // (Hopefully not too expensive...?)
        float slideSpeed = (anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Slide")) ?
            slideSpeedMultiplier :
            1.0f;

        // Calculate movement.
        // Keep the rigidbody's y velocity, but replace the x with our own calculated one.
        Vector2 movement = new Vector2(mx * movementSpeed * slideSpeed, rb.velocity.y);

        // Apply movement.
        rb.velocity = movement;

        // Flip check.
        if (movementSpeed < 0 && isFacingRight || movementSpeed > 0 && !isFacingRight) { 
            Flip();
        }

        // Running check.
        if (Mathf.Abs(mx) > runSpeedThreshold) // Turn whatever value into a positive. 
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

    }

    protected void Flip()
    {
        Vector3 playerScale = transform.localScale;
        playerScale.x = playerScale.x * -1;
        transform.localScale = playerScale;

        //transform.Rotate(0, 180, 0);

        isFacingRight = !isFacingRight;
    }

    protected void Jump()
    {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        rb.velocity = movement;

         anim.SetTrigger("isJumping");
    }
    protected void Slide()
    {
        // Set the "sliding" animation via trigger!
        // The Animator will take care of getting us out of that state.
        anim.SetTrigger("isSliding");
    }

    public bool isGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);
        return (groundCheck != null);
    }

    



}   