using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
	private float h;
	public GameObject playerObject;
	public float viewRange;
	public float viewHeight;
	public GameObject viewObject;

	[Header("Patrol")]
	public float leftPatrolPointx;
	public float rightPatrolPointx;
	public float stayTime;
	private float staytimer;
	private bool staying;
	public bool controlled;
	private MoveController beController;
	void SpotPlayer()
	{
		float targetViewPoint = transform.position.x + (transform.localScale.x > 0 ? 1 : -1) * viewRange;
		
		//! only for test
		if((transform.localScale.x > 0 && 
			targetViewPoint >= playerObject.transform.position.x && playerObject.transform.position.x >= transform.position.x && 
			playerObject.transform.position.y  <= transform.position.y - h / 2 + viewHeight) ||
			( transform.localScale.x < 0 && 
			  targetViewPoint <= playerObject.transform.position.x && playerObject.transform.position.x <= transform.position.x &&
			  playerObject.transform.position.y  >= transform.position.y - h / 2 + viewHeight))
		{
			// TODO show the ending
			viewObject.GetComponent<SpriteRenderer>().color = new Color(1,0,0);
		}
		else
		{
			viewObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
		}
	}
    // Start is called before the first frame update
    void Start()
    {
		beController = GetComponent<MoveController>();
		staying = false;
		h = this.GetComponent<SpriteRenderer>().sprite.rect.height / 100 * transform.localScale.y;
		Debug.Log(h);
	}
	private void FixedUpdate() 
	{
		SpotPlayer();
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
					staying = false;
				}
				else
				{
					staytimer += Time.deltaTime;
					return;
				}
			}

			if(transform.localScale.x > 0)
			{
				if(transform.position.x < rightPatrolPointx)
				{
					beController.MoveRight();
				}
				else
				{
					staying = true;
					staytimer = 0;
					beController.Stop();
				}
			}
			else
			{
				if(transform.position.x > leftPatrolPointx)
				{
					beController.MoveLeft();
				}
				else
				{
					staying = true;
					staytimer = 0;
					beController.Stop();
				}
			}
		}
    }

}
