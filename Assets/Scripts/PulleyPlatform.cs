using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleyPlatform : MonoBehaviour
{
	protected GameObject player;
	protected GameObject[] objs;
	protected PolygonCollider2D collider;
	protected Transform trans;
	public int triggerNum;
	protected int currentCount;
	public Transform[] sourceTransforms;
	public Transform[] targetTransforms;
	public float deltaY;
	public float duration;
	protected float triggerTime;
	protected AudioSource audioSrc;

	void Start()
	{
		triggerTime = -1;
		if (duration == 0) duration = 1;
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		objs = GameObject.FindGameObjectsWithTag("Enermy");
		collider = gameObject.GetComponent<PolygonCollider2D>() as PolygonCollider2D;
		trans = gameObject.GetComponent<Transform>() as Transform;
		if (player == null || collider == null || trans == null) return;

		audioSrc = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
		audioSrc.clip = SFXUtils.GetClip(SFXUtils.Clips.Pulley);
	}
	
	public bool CheckCollide(GameObject obj)
	{
		Collider2D objCollider = obj.GetComponent<Collider2D>() as Collider2D;
		Transform objTrans = obj.GetComponent<Transform>() as Transform;
		return collider.IsTouching(objCollider) && objTrans.position.y >= trans.position.y;
	}

	public float Ease(float x)
	{
		return x * x;
	}

	void FixedUpdate()
	{
		int count = 0;
		if (CheckCollide(player)) count++;
		foreach (GameObject obj in objs)
			if (CheckCollide(obj)) count++;
		currentCount = count;
		if (count >= triggerNum && triggerTime == -1)
		{
			triggerTime = 0;
			audioSrc.volume = 1;
			audioSrc.Play();
		}
		if (triggerTime >= 0)
		{
			float newTime = triggerTime + Time.deltaTime;
			float r1 = Mathf.Min(triggerTime / duration, 1);
			float r2 = Mathf.Min(newTime / duration, 1);
			foreach (Transform transform in sourceTransforms)
				transform.Translate(Vector3.down * deltaY * (Ease(r2) - Ease(r1)));
			foreach (Transform transform in targetTransforms)
				transform.Translate(Vector3.up * deltaY * (Ease(r2) - Ease(r1)));
			if (newTime >= duration && newTime < duration + 0.1f)
				audioSrc.volume = (duration + 0.1f - newTime) / 0.1f;
			else if (triggerTime < duration + 0.1f && newTime >= duration + 0.1f)
				audioSrc.Stop();
			triggerTime = newTime;
		}
	}
}
