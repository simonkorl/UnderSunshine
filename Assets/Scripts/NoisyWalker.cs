using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisyWalker : MonoBehaviour
{
    protected Rigidbody2D body;
    protected MoveController controller;

    public float stepInterval;
    protected float timeSinceLastStep;

    public bool isWorker;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        controller = GetComponent<MoveController>();
        timeSinceLastStep = stepInterval / 2;
    }

    void Update()
    {
        if (controller.grounded && Mathf.Abs(body.velocity.x) >= 0.01)
        {
            timeSinceLastStep += Time.deltaTime;
            if (timeSinceLastStep >= stepInterval)
            {
                timeSinceLastStep = 0;
                string tag = controller.groundTag;
                if (isWorker)
                {
                    float distance = Vector3.Distance(body.transform.position, Camera.main.transform.position);
                    float volume = Mathf.Clamp((25 - distance) / (25 - 15) * 0.8f, 0, 0.8f);
                    if (tag == "MetalPlatform")
                        SFXUtils.PlayRandomOnce(SFXUtils.Clips.WalkMetal1, SFXUtils.Clips.WalkMetal6, volume);
                    else
                        SFXUtils.PlayRandomOnce(SFXUtils.Clips.WalkPatrol1, SFXUtils.Clips.WalkPatrol6, volume);
                }
                else
                {
                    if (tag == "WoodenBox")
                        SFXUtils.PlayRandomOnce(SFXUtils.Clips.WalkWood1, SFXUtils.Clips.WalkWood6, 1.0f);
                    else if (tag == "MetalPlatform")
                        SFXUtils.PlayRandomOnce(SFXUtils.Clips.WalkMetal1, SFXUtils.Clips.WalkMetal6, 1.0f);
                    else
                        SFXUtils.PlayRandomOnce(SFXUtils.Clips.WalkHard1, SFXUtils.Clips.WalkHard6, 1.0f);
                }
            }
        }
        else
        {
            timeSinceLastStep = stepInterval / 2;
        }
    }
}
