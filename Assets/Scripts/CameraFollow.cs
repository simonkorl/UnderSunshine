using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public float followingSpeed = 5f;

    public GameObject player = null;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
            return;
    	float delta = player.transform.position.x - transform.position.x;
        transform.Translate(new Vector3(delta * followingSpeed * Time.deltaTime, 0, 0));
    }
}
