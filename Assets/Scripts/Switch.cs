using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
	protected GameObject player;
	protected BoxCollider2D playerCollider;
	protected PlayerSkill skill;
	protected BoxCollider2D collider;

	public GameObject target;
	public SpriteRenderer spriteRenderer;
	public Sprite triggeredSprite;

	protected bool isTriggered;

	void Start()
	{
		player = GameObject.FindGameObjectsWithTag("Player")[0];
		collider = gameObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
		if (player == null || collider == null) return;
		playerCollider = player.GetComponent<BoxCollider2D>() as BoxCollider2D;
		skill = player.GetComponent<PlayerSkill>() as PlayerSkill;
		if (playerCollider == null || skill == null) return;
		collider.isTrigger = true;

		isTriggered = false;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider != playerCollider) return;
		skill.switchTriggerEvent.AddListener(Trigger);
	}

	protected void Trigger()
	{
		if (isTriggered) return;
		spriteRenderer.sprite = triggeredSprite;
		if (target != null) target.SendMessage("startMoving");
		isTriggered = true;
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider != playerCollider) return;
		skill.switchTriggerEvent.RemoveListener(Trigger);
	}
}
