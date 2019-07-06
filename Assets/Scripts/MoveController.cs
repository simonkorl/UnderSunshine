using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
	[Header("Move")]
	public float moveSpeed;
	public float jumpHeight;
	private float moveVelocity;
	public float dampTime;
	private float dampVelocity;
	
	[Header("GroundDetect")]

	public Transform groundCheck;
	public Vector2 groundDetectSize;
	public LayerMask WhatIsGround;
	private bool grounded;
	
	public int Move()
	{
		// 跳跃
        if(Input.GetKey(KeyCode.Space) && grounded == true)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,jumpHeight);
		}

		// 向右移动
		if (Input.GetKey (KeyCode.RightArrow)) {
			
			moveVelocity = moveSpeed;
			//transform.localScale = new Vector3 (1f, 1f, 1f);

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
			//transform.localScale = new Vector3 (-1f, 1f, 1f);

		}

		//松开移动按键后，让角色滑行一段距离
		if (Input.GetKeyUp (KeyCode.LeftArrow)) 
		{
			
			moveVelocity = 0f;
			//Debug.Log (GetComponent<Rigidbody2D> ().velocity);
		}

		float speedx = Mathf.SmoothDamp (GetComponent<Rigidbody2D> ().velocity.x, moveVelocity, ref dampVelocity, dampTime);
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speedx, GetComponent<Rigidbody2D> ().velocity.y);
		
		if(speedx > 0)
		{
			return 1;
		}
		else if(speedx < 0)
		{
			return 0;
		}
		else
		{
			return -1;
		}
	}
	public void Copy(MoveController controller)
	{
		moveSpeed = controller.moveSpeed;
		jumpHeight = controller.jumpHeight;
		dampTime = controller.dampTime;
	}
    // Start is called before the first frame update
    void Start()
    {
        groundCheck = GetComponent<Transform>();
    }

	private void FixedUpdate() 
	{
		grounded = Physics2D.OverlapBox(groundCheck.position,groundDetectSize,0,WhatIsGround);
	}
	// Update is called once per frame
    void Update()
    {
    }
}
