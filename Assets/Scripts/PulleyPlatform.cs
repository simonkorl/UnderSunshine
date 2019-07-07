using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleyPlatform : MonoBehaviour
{
	public GameObject player;
	public GameObject[] objs;
	public BoxCollider2D collider;
	public Transform trans;
	public int triggerNum;
	public int currentCount;
	public Transform[] sourceTransforms;
	public Transform[] targetTransforms;
	public float deltaY;
	public float duration;
	public float triggerTime;

	void Start()
	{
		triggerTime = -1;
		if (duration == 0) duration = 1;
	}
	
	protected bool CheckCollide(GameObject obj)
	{
		BoxCollider2D objCollider = obj.GetComponent<BoxCollider2D>() as BoxCollider2D;
		Transform objTrans = obj.GetComponent<Transform>() as Transform;
		return collider.IsTouching(objCollider) && objTrans.position.y >= trans.position.y;
	}

	protected float Ease(float x)
	{
		return x * x;
	}

	void FixedUpdate()
	{
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		objs = GameObject.FindGameObjectsWithTag("Enermy");
		collider = gameObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
		trans = gameObject.GetComponent<Transform>() as Transform;
		if (player == null || collider == null || trans == null) return;
		int count = 0;
		if (CheckCollide(player)) count++;
		foreach (GameObject obj in objs)
			if (CheckCollide(obj)) count++;
		currentCount = count;
		if (count >= triggerNum && triggerTime == -1) triggerTime = 0;
		if (triggerTime >= 0)
		{
			float newTime = Math.Min(triggerTime + Time.deltaTime, duration);
			float r1 = triggerTime / duration;
			float r2 = newTime / duration;
			foreach (Transform transform in sourceTransforms)
				transform.Translate(Vector3.down * deltaY * (Ease(r2) - Ease(r1)));
			foreach (Transform transform in targetTransforms)
				transform.Translate(Vector3.up * deltaY * (Ease(r2) - Ease(r1)));
			triggerTime = newTime;
		}
	}
}
