using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Player Player;
	public GameObject currentObject;
	public UnityEngine.UI.Slider test_Slider; //! only for test
	// public static GameManager instance;

	void Awake()
	{
		Player = FindObjectOfType<Player>();
		currentObject = Player.gameObject;
		Player.controlled = true;

		MoveController[] moveObjects = FindObjectsOfType<MoveController>(); 
		foreach (var moveObject in moveObjects)
		{
			moveObject.GetComponent<BasicEnemy>().controlled = false;
		}

	}

	public void setControll(GameObject current)
	{
		//? we might need to add camera tracking when we change the control
		setControllable(currentObject, false);
		currentObject = current;
		setControllable(currentObject, true);
	}
	//! only for test
	public void showSkillBar(float ratio) 
	{
		//TODO make a real skill progress bar
		test_Slider.value = ratio;
	}
	void setControllable(GameObject current, bool controllable)
	{
		if(current.name == Player.gameObject.name)
			current.GetComponent<Player>().controlled = controllable;
		else
		{
			current.GetComponent<BasicEnemy>().controlled = controllable;
			if(controllable == false)
			{
				current.GetComponent<MoveController>().canMove = false;
				current.GetComponent<BasicEnemy>().canBeControlled = false;
			}
		}
			
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
