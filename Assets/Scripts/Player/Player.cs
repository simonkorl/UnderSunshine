using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	MoveController playerMove;
	public bool controlled;
	void Awake()
	{
		playerMove = GetComponent<MoveController>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(controlled)
		{
			playerMove.canMove = true;
			playerMove.ControlMove();
		}
		else{
			playerMove.canMove = false;
		}
	}
}
