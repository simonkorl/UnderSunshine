using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnermy : MonoBehaviour
{
	public float viewRange;
	public float viewHeight;
	private float viewDirect;
	public Transform[] movePointset;
	public bool controlled;
	public MoveController beController;
    // Start is called before the first frame update
    void Start()
    {
		beController = GetComponent<MoveController>();
		MoveController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveController>();
		beController.Copy(playerController);
	}

    // Update is called once per frame
    void Update()
    {
        if(controlled)
		{
			beController.Move();
		}
    }
}
