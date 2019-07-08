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

	public AudioSource audioCarStart;
	public AudioSource audioCarRunning;
	public AudioSource audioCarStop;
	protected float totalTime;

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
	private float LinearEnvelope(float t, float t1, float t2, float v1, float v2)
	{
		// Mathf.Clamp does not work since v1 may be greater than v2
		if (t <= t1) return v1;
		if (t >= t2) return v2;
		return (t - t1) / (t2 - t1) * (v2 - v1) + v1;
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
			operated = true;
			totalTime = 0;
			audioCarStart.volume = 1;
			audioCarStart.Play();
			audioCarRunning.Play();
		}
		if (operated)
		{
			float totalTimePrev = totalTime;
			totalTime += Time.deltaTime;
			audioCarRunning.volume =    // (/_ ;)
				totalTime < 4 ?
				LinearEnvelope(totalTime, 1.5f, 1.6f, 0, 0.7f) :
				LinearEnvelope(totalTime, 6.4f, 6.6f, 0.7f, 0f);
			if (totalTimePrev < 1.6f && totalTime >= 1.6f)
			{
				truckMove.startMoving();
			}
			else if (totalTimePrev < 6.4f && totalTime >= 6.4f)
			{
				audioCarStop.volume = 1;
				audioCarStop.Play();
			}
		}
    }
}
