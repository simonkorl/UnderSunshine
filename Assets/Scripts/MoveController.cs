﻿using System.Collections;
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

	public string groundTag;
	protected bool firstTimeGround;

	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		firstTimeGround = true;
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
		if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && grounded)
		{
			velocity.y = jumpHeight * 1.4f;
			if(animator != null)
				animator.SetTrigger("Jump");
			SFXUtils.PlayOnce(SFXUtils.Clips.Jump, 1.0f);
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
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		if (Mathf.Abs(body.velocity.y) > jumpHeight * 1.4f * 0.55f)
			body.gravityScale = 2.0f;
		//if (body.velocity.y != 0) Debug.Log(body.velocity.y);
		if (!canMove)
		{
			body.velocity = new Vector2(0, body.velocity.y);
		}
		Collider2D collider = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, WhatIsGround).collider;
		if (collider != null)
		{
			if (!grounded)
			{
				if (firstTimeGround) firstTimeGround = false;
				else SFXUtils.PlayOnce(SFXUtils.Clips.Fall, 1.0f);
			}
			grounded = true;
			groundTag = collider.gameObject.tag;
		}
		else
		{
			grounded = false;
		}
		if(animator != null)
			animator.SetBool("Hovering", !grounded);
	}
}
