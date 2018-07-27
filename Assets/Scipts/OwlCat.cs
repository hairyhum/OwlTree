using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction { Left, Right };

public class OwlCat : MonoBehaviour {

    Direction direction = Direction.Right;
    float speed = 0.0f;
    float jumpSpeed = 20f;
    float runSpeed = 10f;

    bool grounded = false;
    float groundCheckRadius = 0.2f;

    public LayerMask ground;
    public Transform groundCheck;

    // Use this for initialization
	void Start () {
		
	}

	private void FixedUpdate()
	{
        var foo = 1;
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
        if(grounded){
            GetComponent<SpriteRenderer>().color = Color.white;
        } else {
            GetComponent<SpriteRenderer>().color = Color.grey;
        }
        var control = Input.GetAxis("Horizontal");

        speed = Math.Abs(control * runSpeed);
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(control * runSpeed, rb.velocity.y);

        Turn(control);
	}

    void Turn(float control) {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        if (control < 0)
        {
            direction = Direction.Left;
            spriteRenderer.flipX = true;
        }
        else if (control > 0)
        {
            direction = Direction.Right;
            spriteRenderer.flipX = false;
        }
    }

	// Update is called once per frame
	void Update () {
        var jumpButton = Input.GetKeyDown(KeyCode.Space);
        if(jumpButton && grounded){
            var rb = GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
	}
}
