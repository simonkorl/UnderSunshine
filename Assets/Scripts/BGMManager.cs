using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public AudioSource minor;
    public AudioSource major;

    public float rampDur;

    protected bool currentIsMinor;
    protected float rampFrom, rampTo;
    protected float rampUntil;

    void Start()
    {
        Screen.SetResolution(1920, 1080, false);

        DontDestroyOnLoad(this);

        minor.loop = true;
        minor.Play();
        major.loop = true;
        major.Play();
        currentIsMinor = true;
        rampFrom = 0;
        rampTo = 0;
        rampUntil = 0;
    }

    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        string name = scene.name;
        Debug.Log(name);
        bool isMinor = (name == "Level1" || name == "Level2" || name == "Level3" || name == "3");
        if (isMinor != currentIsMinor)
        {
            rampFrom = (currentIsMinor ? 0 : 1);
            rampTo = (isMinor ? 0 : 1);
            rampUntil = rampDur;
            currentIsMinor = isMinor;
        }
        float vol;
        if (rampUntil > 0)
        {
            rampUntil = Math.Max(rampUntil - Time.deltaTime, 0);
            vol = Mathf.Lerp(rampFrom, rampTo, 1.0f - rampUntil / rampDur);
        }
        else
        {
            vol = (currentIsMinor ? 0 : 1);
        }
        major.volume = vol;
        minor.volume = 1.0f - vol;
        /*Debug.Log("a");
        Debug.Log(rampFrom);
        Debug.Log(rampTo);
        Debug.Log(rampUntil);
        Debug.Log(vol);*/
    }
}
