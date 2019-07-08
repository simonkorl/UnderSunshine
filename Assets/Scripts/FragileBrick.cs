using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileBrick : MonoBehaviour
{
	bool touched = false;
	public float delay = 1f;

	public GameObject sprite;
	public GameObject sprite1;
	public GameObject sprite2;

	IEnumerator Break()
	{
		yield return new WaitForSeconds(delay);
		SpriteRenderer renderer = sprite.GetComponent<SpriteRenderer>() as SpriteRenderer;
		renderer.enabled = false;
		sprite1.SetActive(true);
		sprite2.SetActive(true);
		gameObject.SetActive(false);
		ParticleSystem sys = sprite.GetComponent<ParticleSystem>() as ParticleSystem;
		if (sys != null) sys.Play();
		SFXUtils.PlayOnce(SFXUtils.Clips.Brickbreaking, 1.0f);
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
    	if(!touched && collision.transform.gameObject.name == "Player")
    	{
    		touched = true;
    		StartCoroutine(Break());
    	}
    }
}
