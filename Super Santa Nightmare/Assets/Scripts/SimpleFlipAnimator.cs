using UnityEngine;
using System.Collections;

public class SimpleFlipAnimator : MonoBehaviour
{
    public int AnimFrameLength = 10;
    public bool Active = true;

    private SpriteRenderer Renderer;
    private int AnimCounter = 0;

    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (Active)
        {
            if (++AnimCounter >= AnimFrameLength)
            {
                Renderer.flipX = !Renderer.flipX;
                AnimCounter = 0;
            }
        }
    }
}
