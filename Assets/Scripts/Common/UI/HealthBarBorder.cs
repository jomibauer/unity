using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBorder : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float transitionSpeed;
    float timeLeft;
    Color targetColor;
    Color startColor;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetColor = Color.green;
        startColor = Color.white;
        timeLeft = transitionSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= Time.deltaTime)
        {
            spriteRenderer.material.color = targetColor;
            SwapColors();
            timeLeft = transitionSpeed;
        }
        else
        {
            // transition in progress
            // calculate interpolated color
            spriteRenderer.material.color = Color.Lerp(spriteRenderer.material.color, targetColor, Time.deltaTime / timeLeft);
        
            // update the timer
            timeLeft -= Time.deltaTime;
        }
    }

    void SwapColors()
    {
        Color temp = targetColor;
        targetColor = startColor;
        startColor = temp;
    }
}
