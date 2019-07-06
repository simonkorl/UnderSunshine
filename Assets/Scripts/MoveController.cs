using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
	[Header("Move")]
	public float moveSpeed;
	public float jumpHeight;
	public bool canMove;
	private float moveVelocity;
	public float dampTime;
	private float dampVelocity;
	
	[Header("GroundDetect")]
	public float groundRadius;
	public LayerMask WhatIsGround;
	public bool grounded;

	private Animator animator;

	void Start()
	{
		animator = GetComponentInChildren<Animator>();
	}
	
	public void ControlMove()
	{
		if(!canMove)
			return;

		float horizontal = Input.GetAxis("Horizontal");
		moveVelocity = horizontal * moveSpeed;

		if(horizontal != 0)
		{
			transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x) * (horizontal < 0 ? -1 : 1), transform.localScale.y, transform.localScale.z);
			if(animator != null)
				animator.SetBool("Moving", true);
		}
		else
		{
			if(animator != null)
				animator.SetBool("Moving", false);
		}

		Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
		velocity.x = Mathf.SmoothDamp(velocity.x, moveVelocity, ref dampVelocity, dampTime);
		if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded)
		{
			velocity.y = jumpHeight;
			if(animator != null)
				animator.SetTrigger("Jump");
		}
		GetComponent<Rigidbody2D>().velocity = velocity;
	}
	public void Copy(MoveController controller)
	{
		moveSpeed = controller.moveSpeed;
		jumpHeight = controller.jumpHeight;
		dampTime = controller.dampTime;
	}
	public void MoveLeft()
	{
		if(!canMove)
			return;
		GetComponent<Rigidbody2D>().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		transform.localScale = new Vector3 (transform.localScale.x < 0 ? transform.localScale.x : -transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	public void MoveRight()
	{
		if(!canMove)
			return;
		GetComponent<Rigidbody2D>().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		transform.localScale = new Vector3 (transform.localScale.x > 0 ? transform.localScale.x : -transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	public void Jump()
	{
		if(!canMove)
			return;
		if(grounded)
		{
			grounded = false;
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,jumpHeight);
		}
	}
	public void Stop() 
	{
		if(!canMove)
			return;
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
	}

	void FixedUpdate() 
	{
		if(Physics2D.Raycast(transform.position, Vector2.down, 0.1f, WhatIsGround).collider != null)
			grounded = true;
		else
			grounded = false;
		if(animator != null)
			animator.SetBool("Hovering", !grounded);
	}
}
