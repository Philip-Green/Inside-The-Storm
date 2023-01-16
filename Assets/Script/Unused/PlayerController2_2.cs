using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float movement;
	[SerializeField] Rigidbody2D rigid;
    [SerializeField] Collider2D collider2;
	[SerializeField] float speed = 10.0f;
	[SerializeField] bool isFacingRight = true;
	[SerializeField] float jumpForce = 500.0f;
	[SerializeField] LayerMask whatIsGround;
    [SerializeField] double groundDistance = 1.5;
    [SerializeField] bool isGrounded = true;
	[SerializeField] bool jumpPressed = false;


    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
		if (rigid == null)
			rigid = GetComponent<Rigidbody2D>();
       // not necessary since we don't use the isOnGround function
        /*if (collider2 == null)
            collider2 = GetComponent<Collider2D>();
        groundDistance = collider2.bounds.extents.y; //based on https://answers.unity.com/questions/196381/how-do-i-check-if-my-rigidbody-player-is-grounded.html */

    }

    // Update is called once per frame; good for user input
    void Update()
    {
		movement = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump"))
		{
			jumpPressed = true;
		}
    }

    //called potentially multiple times per frame, best for physics for smooth behavior
    void FixedUpdate()
	{
		rigid.velocity = new Vector2(movement * speed, rigid.velocity.y);
		if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
			Flip();

		if (jumpPressed && isGrounded)
			Jump();
	}

    void Flip()
	{
		Vector3 playerScale = transform.localScale;
		playerScale.x = playerScale.x * -1;
		transform.localScale = playerScale;

        //transform.Rotate(0, 180, 0);

        isFacingRight = !isFacingRight;
	}

    void Jump()
	{
		rigid.velocity = new Vector2(rigid.velocity.x, 0);
		rigid.AddForce(new Vector2(0, jumpForce));
		jumpPressed = false;
        isGrounded = false;
		
	}

    bool isOnGround()
	{
        //not using in this game in favor of collision detection. More on this function here: https://kylewbanks.com/blog/unity-2d-checking-if-a-character-or-object-is-on-the-ground-using-raycasts
        Debug.Log("is on ground called");
		Vector2 position = transform.position;
		Vector2 direction = Vector2.down;

		//for debugging purposes only
		Debug.DrawRay(position, direction, Color.green);

		//Debug.Log("drew ray");

		RaycastHit2D hit = Physics2D.Raycast(position, direction, (float)groundDistance, whatIsGround);
		Debug.Log(hit.collider.name);
        if (hit.collider != null)
            return true;
        else
            return false; 
            
	}

    
    void OnCollisionEnter2D (Collision2D collider)
    //inspiration from here: https://answers.unity.com/questions/196381/how-do-i-check-if-my-rigidbody-player-is-grounded.html
    {
        if (collider.gameObject.tag == "Ground")
            isGrounded = true;
        
    }

   
}
