using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBlink : MonoBehaviour
{
    Image ui_image;
    public float transitionSpeed;
    float timeLeft;
    Color targetColor;
    Color startColor;
    void Start()
    {
        ui_image = GetComponent<Image>();
        targetColor = Color.green;
        startColor = Color.white;
        timeLeft = transitionSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= Time.deltaTime)
        {
            ui_image.color = targetColor;
            SwapColors();
            timeLeft = transitionSpeed;
        }
        else
        {
            // transition in progress
            // calculate interpolated color
            ui_image.color = Color.Lerp(ui_image.color, targetColor, Time.deltaTime / timeLeft);
        
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
