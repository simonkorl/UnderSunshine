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
    
    protected const float BAR_LENGTH = 60.0f / 72 * 4;
    protected int lastBar;

    void Start()
    {
        Screen.SetResolution(1600, 900, false);

        DontDestroyOnLoad(this);

        minor.loop = true;
        minor.Play();
        major.loop = true;
        major.Play();
        currentIsMinor = true;
        rampFrom = 0;
        rampTo = 0;
        rampUntil = 0;
        lastBar = -1;
    }

    // (exp(0.5 k) - exp(0)) / m ==
    // (exp(k) - exp(0.5 k)) / (1 - m)
    static private float VOL_MIDWAY = 0.2f;
    static private float K = 2 * Mathf.Log(1.0f / VOL_MIDWAY - 1);
    static private float DENOM_INV = 1.0f / (Mathf.Exp(K) - 1);

    static float EaseExpOut(float x)
    {
        return (Mathf.Exp(K * x) - 1) * DENOM_INV;
    }

    void Update()
    {
        int curBar = (int)Mathf.Floor(Mathf.Repeat(minor.time + rampDur, minor.clip.length) / BAR_LENGTH);
        if (curBar != lastBar)
        {
            Debug.Log(curBar);
            Scene scene = SceneManager.GetActiveScene();
            string name = scene.name;
            bool isMinor = (name == "Level1" || name == "Level2" || name == "Level3" || name == "3");
            if (isMinor != currentIsMinor)
            {
                rampFrom = (currentIsMinor ? 0 : 1);
                rampTo = (isMinor ? 0 : 1);
                rampUntil = rampDur;
                currentIsMinor = isMinor;
            }
            lastBar = curBar;
        }
        float vol;
        if (rampUntil > 0)
        {
            rampUntil = Math.Max(rampUntil - Time.deltaTime, 0);
            vol = Mathf.Lerp(rampFrom, rampTo, EaseExpOut(1.0f - rampUntil / rampDur));
        }
        else
        {
            vol = (currentIsMinor ? 0 : 1);
        }
        major.volume = vol;
        minor.volume = 1.0f - vol;
    }
}
