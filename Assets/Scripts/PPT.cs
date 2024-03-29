﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PPT : MonoBehaviour
{
    public SpriteRenderer[] cgs;
    public string nextLevel;
    float duration = 1f;
    int current = 0;

    IEnumerator Play()
    {
    	yield return new WaitForSeconds(2.0f);
    	for(int i = 0; i < cgs.Length; i++)
    	{
    		while(!Input.anyKeyDown)
	        	yield return 0;
	        SpriteRenderer cg = cgs[i];
	        for(float t = 0; t < duration; t += Time.deltaTime)
	        {
	        	cg.color = new Color(1.0f, 1.0f, 1.0f, 1 - t / duration);
	        	yield return 0;
	        }
	        cg.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    	}
    	if(nextLevel != "")
    		SceneManager.LoadScene(nextLevel);
    	else
    		Application.Quit();
    }

    void Start()
    {
    	StartCoroutine(Play());
    }
}
