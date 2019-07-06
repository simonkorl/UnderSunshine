﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnermy : MonoBehaviour
{
	public float viewRange;
	public float viewHeight;
	public Vector2 leftPatrolPoint;
	public Vector2 rightPatrolPoint;
	public bool movingRight;
	public float stayTime;
	private float staytimer;
	private bool staying;
	public bool controlled;
	public MoveController beController;
    // Start is called before the first frame update
    void Start()
    {
		beController = GetComponent<MoveController>();
	}

    // Update is called once per frame
    void Update()
    {

		// 如果被控制，则玩家操控
        if(controlled)
		{
			beController.ControlMove();
			// 如果有可以交互的道具
			// 则可以交互
		}
		else
		{ // 如果不被控制，则自己行动
			if(staying)
			{
				if(staytimer >= stayTime)
				{
					transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localPosition.z);
				}
				else
				{
					staytimer += Time.deltaTime;
					return;
				}
			}

			if(transform.localScale.x > 0)
			{
				if(transform.position.x < rightPatrolPoint.x)
				{
					beController.MoveRight();
				}
				else
				{
					staying = true;
					beController.Stop();
				}
			}
			else
			{
				if(transform.position.x > leftPatrolPoint.x)
				{
					beController.MoveLeft();
				}
				else
				{
					staying = true;
					beController.Stop();
				}
			}
		}
    }
}
