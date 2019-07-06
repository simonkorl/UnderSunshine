using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	MoveController playerMove;
	public bool controlled;

	[Header("Death Judge")]
	GameManager gameManager;
	public Transform face;

	void Awake()
	{
		playerMove = GetComponent<MoveController>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if(controlled)
		{
			playerMove.canMove = true;
			playerMove.ControlMove();
		}
		else
		{
			playerMove.canMove = false;
		}

		var hit = Physics2D.Raycast(face.position, 
									new Vector2(Mathf.Cos(77.71f * Mathf.Deg2Rad), Mathf.Sin(77.71f * Mathf.Deg2Rad)), 
									Mathf.Infinity, 
									LayerMask.GetMask("Floor") | LayerMask.GetMask("Hole") | LayerMask.GetMask("Shelter"));
		if(hit.collider == null || LayerMask.LayerToName(hit.transform.gameObject.layer) == "Hole")
			gameManager.GameOver();
	}
}
