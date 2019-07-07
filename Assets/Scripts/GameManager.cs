using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Player Player;
	public GameObject winFlag;
	public string nextScene;
	public GameObject currentObject;
	public UnityEngine.UI.Slider test_Slider; //! only for test
	// public static GameManager instance;

	void Awake()
	{
		Player = FindObjectOfType<Player>();
		winFlag = GameObject.Find("WinFlag");
		currentObject = Player.gameObject;
		Player.controlled = true;

		MoveController[] moveObjects = FindObjectsOfType<MoveController>(); 
		foreach (var moveObject in moveObjects)
		{
			var basicEnemy = moveObject.GetComponent<BasicEnemy>();
			if(basicEnemy != null)
				basicEnemy.controlled = false;
		}

	}
	public void winCheck()
	{
		if(Player.transform.position.x >= winFlag.transform.position.x)
		{
			SceneManager.LoadScene(nextScene);
		}
	}
	public void setControll(GameObject current)
	{
		setControllable(currentObject, false);
		currentObject = current;
		setControllable(currentObject, true);
	}
	//! only for test
	public void showSkillBar(float ratio) 
	{
		//TODO make a real skill progress bar
		//test_Slider.value = ratio;
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

	public void GameOver()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
			GameOver();
		winCheck();
	}
}
