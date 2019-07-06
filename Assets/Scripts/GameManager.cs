using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Player Player;
	public GameObject currentObject;

	//public static GameManager instance;

	void Awake()
	{
		Player = FindObjectOfType<Player>();
		Player.controlled = true;
		Debug.Log(Player.controlled);

		MoveController[] moveObjects = FindObjectsOfType<MoveController>(); 
		foreach (var moveObject in moveObjects)
		{
			moveObject.GetComponent<BasicEnemy>().controlled = false;
		}

	}

	public void setControll(GameObject current)
	{
		setControllable(currentObject, false);
		currentObject = current;
		setControllable(currentObject, true);
	}

	void setControllable(GameObject current, bool controllable)
	{
		if(current.name == Player.gameObject.name)
			current.GetComponent<Player>().controlled = controllable;
		else
			current.GetComponent<BasicEnemy>().controlled = controllable;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
