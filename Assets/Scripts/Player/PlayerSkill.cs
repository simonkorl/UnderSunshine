using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour {
	public Transform enemyCheck;
	public Vector2 enemyDetectSize;
	public LayerMask WhatIsEnemy;
	GameManager manager;

	void Awake()
	{
		manager = FindObjectOfType<GameManager>();
	}


	void useControllSkill()
	{
		GameObject target = null;
		Collider2D[] detects = Physics2D.OverlapBoxAll(enemyCheck.position,enemyDetectSize,0,WhatIsEnemy);
		foreach (var enemyCollider in detects)
		{
			if(enemyCollider.gameObject.GetComponent<BasicEnemy>() != null)
			{
				target = enemyCollider.gameObject;
				break;
			}
		}
		if(target != null)
		{
			manager.setControll(target);
		}
	}

	void takeBackControll()
	{
		manager.setControll(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Z))
		{
			if(GetComponent<Player>().controlled)
				useControllSkill();
			else
				takeBackControll();
		}
	}
}
