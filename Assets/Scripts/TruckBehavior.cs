using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckBehavior : MonoBehaviour
{
	public Transform driverCheck;
	public Vector2 driverDetectSize;
	public LayerMask WhatIsDriver;
	private MovingPlatform truckMove;
	Collider2D[] detects;
	GameObject driver;
	private SpriteRenderer sRenderer;
	private GameObject player;
	bool operated = false;
    // Start is called before the first frame update
    void Start()
    {
        truckMove = GetComponent<MovingPlatform>();
		sRenderer = GetComponent<SpriteRenderer>();
		player = GameObject.Find("Player");
		sRenderer.enabled = false;
    }
	void findTarget()
	{
		driver = null;
		detects = Physics2D.OverlapBoxAll(driverCheck.position,driverDetectSize,0,WhatIsDriver);
		foreach (var driverCollider in detects)
		{
			if(driverCollider.gameObject.GetComponent<BasicEnemy>() != null && driverCollider.gameObject.GetComponent<BasicEnemy>().canBeControlled)
			{
				driver = driverCollider.gameObject;
				break;
			}
		}
	}
	private void FixedUpdate() {
		findTarget();
	}
    // Update is called once per frame
    void Update()
    {
		if(!operated && Input.GetKeyDown(KeyCode.X) && driver != null)
		{
			sRenderer.enabled = true;
			player.GetComponent<PlayerSkill>().takeBackControll();
			Destroy(driver);
			// change the image
			truckMove.startMoving();
			operated = true;
		}
    }
}
