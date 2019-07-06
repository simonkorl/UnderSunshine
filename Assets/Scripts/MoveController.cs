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

	public Transform groundCheck;
	public float groundRadius;
	public LayerMask WhatIsGround;
	private bool grounded;
	
	public void ControlMove()
	{
		if(!canMove)
			return;
		// 跳跃
        if(Input.GetKey(KeyCode.Space) && grounded == true)
		{
			grounded = false;
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,jumpHeight);
		}

		// 向右移动
		if (Input.GetKey (KeyCode.RightArrow)) 
		{		
			moveVelocity = moveSpeed;
			transform.localScale = new Vector3 (transform.localScale.x > 0 ? transform.localScale.x : -transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}

		// 松开移动按键后，让角色滑行一段距离
		if (Input.GetKeyUp (KeyCode.RightArrow)) 
		{	
			moveVelocity = 0f;
			//Debug.Log (GetComponent<Rigidbody2D> ().velocity);
		}


		//向左移动
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{		
			moveVelocity = -moveSpeed;
			transform.localScale = new Vector3 (transform.localScale.x < 0 ? transform.localScale.x : -transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}

		//松开移动按键后，让角色滑行一段距离
		if (Input.GetKeyUp (KeyCode.LeftArrow)) 
		{
			
			moveVelocity = 0f;
			//Debug.Log (GetComponent<Rigidbody2D> ().velocity);
		}

		float speedx = Mathf.SmoothDamp (GetComponent<Rigidbody2D> ().velocity.x, moveVelocity, ref dampVelocity, dampTime);
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speedx, GetComponent<Rigidbody2D> ().velocity.y);
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
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		transform.localScale = new Vector3 (transform.localScale.x < 0 ? transform.localScale.x : -transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	public void MoveRight()
	{
		if(!canMove)
			return;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
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
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
	}
	
    // Start is called before the first frame update
    void Start()
    {
    }

	private void FixedUpdate() 
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position,groundRadius,WhatIsGround);
	}
	// Update is called once per frame
    void Update()
    {
		// if(!canMove && !faintAnime.isPlaying)
		// {
		// 	faintAnime.Play();
		// }
    }
}
