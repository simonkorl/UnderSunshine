using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject Player;
	public GameObject currentObject;

	//public static GameManager instance;

	void Awake()
	{
		MoveController[] moveObjects = FindObjectsOfType<MoveController>(); 
		foreach (var moveObject in moveObjects)
		{
			if(moveObject.gameObject.name == "Player")
				moveObject.enabled = true;
			else
				moveObject.enabled = false;
		}
	}

	public void setControll(GameObject current)
	{
		currentObject.GetComponent<MoveController>().enabled = false;
		currentObject = current;
		current.GetComponent<MoveController>().enabled = true;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
