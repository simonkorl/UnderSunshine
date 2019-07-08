using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
	protected GameObject player;
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
		skill = player.GetComponent<PlayerSkill>() as PlayerSkill;
		if (skill == null) return;
		collider.isTrigger = true;

		isTriggered = false;

		skill.switchTriggerEvent.AddListener(Trigger);
	}

	protected void Trigger()
	{
		if (isTriggered) return;

		GameObject controlledObj = skill.manager.currentObject;
		BoxCollider2D controlledCollider = controlledObj.GetComponent<BoxCollider2D>() as BoxCollider2D;
		if (!collider.IsTouching(controlledCollider)) return;

		spriteRenderer.sprite = triggeredSprite;
		if (target != null) target.SendMessage("startMoving");
		isTriggered = true;
		SFXUtils.PlayOnce(SFXUtils.Clips.Switch, 1.0f);

		if (controlledObj != player)
		{
			skill.takeBackControll();
		}
	}

	void OnDestroy()
	{
		if (skill) skill.switchTriggerEvent.RemoveListener(Trigger);
	}
}
