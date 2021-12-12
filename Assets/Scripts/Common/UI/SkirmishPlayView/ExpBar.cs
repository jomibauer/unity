using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : MonoBehaviour
{
    RectTransform expBarTransform;
    float expUnit = 5.5f;

    void Awake()
    {
        expBarTransform = this.GetComponent<RectTransform>();
    }

    public void Load(float exp)
    {
        this.expBarTransform.sizeDelta = new Vector2(exp * expUnit, 30);
    }

    public void Raise()
    {
        this.expBarTransform.sizeDelta += new Vector2(expUnit, 0);
    }
    
    public void Reset()
    {
        this.expBarTransform.sizeDelta = new Vector2(0, 30);
    }
}
