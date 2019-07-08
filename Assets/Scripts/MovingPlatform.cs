using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float movingDistance;
    public float m_MovingTime;
    Vector3 originPos;
    Vector3 targetPos;
    public bool easeOut;

    public void startMoving()
    {
        StartCoroutine(moving());
    }

    IEnumerator moving()
    {
        transform.position = originPos;
        float currentTime = Time.time;
        while(Time.time - currentTime <= m_MovingTime)
        {
            float r = (Time.time - currentTime)/m_MovingTime;
            if (easeOut) r = (float)Math.Sin(r * Math.PI / 2);
            transform.position = Vector3.Lerp(originPos, targetPos, r);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
        transform.position = targetPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        targetPos = new Vector3(originPos.x + movingDistance, originPos.y, originPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
