using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour {
	public Transform enemyCheck;
	public Vector2 enemyDetectSize;
	public LayerMask WhatIsEnemy;
	public Collision2D enemyDetect;
	public float skillDP = 1;
	private float DPtimer = 0;
	Collider2D[] detects;
	GameObject controlTarget;
	GameManager manager;
	public CameraFollow cameraFollow;

	void Awake()
	{
		manager = FindObjectOfType<GameManager>();
		cameraFollow = GameObject.Find("Cameras").GetComponent<CameraFollow>();
	}
	void findTarget()
	{
		controlTarget = null;
		detects = Physics2D.OverlapBoxAll(enemyCheck.position,enemyDetectSize,0,WhatIsEnemy);
		foreach (var enemyCollider in detects)
		{
			if(enemyCollider.gameObject.GetComponent<BasicEnemy>() != null && enemyCollider.gameObject.GetComponent<BasicEnemy>().canBeControlled)
			{
				controlTarget = enemyCollider.gameObject;
				break;
			}
		}

		//! only for debug
		//* can be replaced by animation
		// if(controlTarget != null)
		// {
		// 	this.GetComponent<SpriteRenderer>().color = new Color(0,1,0);
		// }
		// else
		// {
		// 	this.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
		// }
	}

	void useControllSkill()
	{
		findTarget();
		if(controlTarget != null)
		{
			cameraFollow.player = controlTarget;
			manager.setControll(controlTarget);
		}
	}

	void takeBackControll()
	{
		cameraFollow.player = gameObject;
		manager.setControll(gameObject);
	}
	void FixedUpdate() {
		findTarget();
	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Z))
		{
			if(GetComponent<Player>().controlled)
			{
				if(controlTarget != null)
				{
					DPtimer += Time.deltaTime;
				}
				else
				{
					DPtimer = 0;
				}
			}
			else
			{
				DPtimer += Time.deltaTime;
			}
			
			
			if(DPtimer >= skillDP)
			{
				if(GetComponent<Player>().controlled)
				{
					useControllSkill();
					DPtimer = 0;
				}
				else
				{
					takeBackControll();
					DPtimer = 0;
				}
			}
		}
		else
		{
			DPtimer = 0;
		}
		//! only for test
		//manager.showSkillBar(DPtimer / skillDP);
	}
}
