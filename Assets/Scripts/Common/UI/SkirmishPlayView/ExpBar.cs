using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : MonoBehaviour
{
    RectTransform expBarTransform;
    float expUnit = 5.5f;
    // Start is called before the first frame update
    void Awake()
    {
        expBarTransform = this.GetComponent<RectTransform>();
        
    }

    private void OnAwardExpStart(object arg1, object arg2)
    {
        throw new NotImplementedException();
    }

    public void Load(float exp)
    {
        this.expBarTransform.sizeDelta = new Vector2(1 * expUnit, 30);
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
