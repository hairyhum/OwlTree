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

    public bool grounded = false;
    public bool grapled = false;

    float groundCheckRadius = 0.2f;
    float grapleCheckRadius = 0.5f;

    public LayerMask ground;
    public LayerMask wood;
    public Transform groundCheck;
    public Transform grapleCheck;

    // Use this for initialization
	void Start () {
		
	}

	void FixedUpdate()
	{
        
        var control = Input.GetAxis("Horizontal");

        speed = Math.Abs(control * runSpeed);
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(control * runSpeed, rb.velocity.y);

        Turn(control);
	}

    void CheckGround() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
    }

    void CheckGraple() {
        grapled = !grounded && Physics2D.OverlapCircle(grapleCheck.position, grapleCheckRadius, wood);
    }

    void UpdateColor(){
        if (grounded)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        } else if (grapled) {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        } else {
            GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }

    void Turn(float control) {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        if (control < 0)
        {
            if (direction == Direction.Right)
            {
                direction = Direction.Left;
                FlipDirection();
            }
        }
        else if (control > 0)
        {
            if (direction == Direction.Left)
            {
                direction = Direction.Right;
                FlipDirection();
            }
        }
    }

    void FlipDirection(){
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    bool CanJump() {
        return grounded || grapled;
    }

	// Update is called once per frame
	void Update () {
        CheckGround();
        CheckGraple();
        UpdateColor();

        var jumpButton = Input.GetKeyDown(KeyCode.Space);
        if(jumpButton && CanJump()){
            var rb = GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
	}
}
