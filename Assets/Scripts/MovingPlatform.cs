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
        AudioSource audioMoving = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        AudioSource audioOff = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioMoving.clip = SFXUtils.GetClip(SFXUtils.Clips.ElectricMotor);
        audioOff.clip = SFXUtils.GetClip(SFXUtils.Clips.ElectricMotorOff);

        audioMoving.volume = 1;
        audioMoving.Play();
        transform.position = originPos;
        float currentTime = Time.time;
        bool audioOffStarted = false;
        // How much early should the "off" sound start playing, in seconds
        const float Anticipation = 1.0f;
        while(Time.time - currentTime <= m_MovingTime)
        {
            float r = (Time.time - currentTime)/m_MovingTime;
            if (easeOut) r = Mathf.Sin(r * Mathf.PI / 2);
            transform.position = Vector3.Lerp(originPos, targetPos, r);
            // Audio envelope
            if (Time.time - currentTime >= m_MovingTime - Anticipation)
            {
                // Check for the start of the tail sound
                if (!audioOffStarted)
                {
                    audioOffStarted = true;
                    audioOff.Play();
                }
                audioMoving.volume = (m_MovingTime - (Time.time - currentTime)) / Anticipation;
                audioOff.volume = 1 - audioMoving.volume;
            }
            else
            {
                audioMoving.volume = Mathf.Min(1, (Time.time - currentTime) / 0.1f);
            }
            yield return new WaitForSeconds(0);
        }
        audioOff.volume = 1;
        audioMoving.Stop();
        transform.position = targetPos;
        yield return null;
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
