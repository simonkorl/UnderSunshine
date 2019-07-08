using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisyBox : MonoBehaviour
{
    protected float initialY;

    void Start()
    {
        initialY = gameObject.transform.position.y;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8
            && collision.transform.position.y < initialY - 1.5)
        {
            SFXUtils.PlayOnce(SFXUtils.Clips.WoodenBox, 1.0f);
        }
    }
}
