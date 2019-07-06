using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
	private float w;
	private float h;
	public GameObject playerObject;
	public float viewDistance;
	public float viewHeight;
	public GameObject viewObject;

	[Header("Patrol")]
	public float leftPatrolPointx;
	public float rightPatrolPointx;
	public float stayTime;
	private float staytimer;
	private bool staying;
	public bool controlled;
	public bool canBeControlled;
	private MoveController beController;

	void Awake()
	{
		playerObject = FindObjectOfType<Player>().gameObject;
		canBeControlled = true;
	}

	void SpotPlayer()
	{
		Vector2 origin = new Vector2(transform.position.x + (transform.localScale.x > 0 ? 1 : -1) * w / 2, transform.position.y - h / 2 + h * viewHeight);
		Vector2 direct = new Vector2(transform.localScale.x > 0 ? 1 : -1, 0);
		if(Physics2D.Raycast(origin, direct, viewDistance,LayerMask.GetMask("Player")))
		{
			//* spot player
			//! only for debug
			this.GetComponent<SpriteRenderer>().color = new Color(1,0,0);
		}
		else
		{
			this.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
		}
	}
    // Start is called before the first frame update
    void Start()
    {
		beController = GetComponent<MoveController>();
		staying = false;
		w = this.GetComponent<SpriteRenderer>().sprite.rect.width / 100 * transform.localScale.x;
		h = this.GetComponent<SpriteRenderer>().sprite.rect.height / 100 * transform.localScale.y;
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
