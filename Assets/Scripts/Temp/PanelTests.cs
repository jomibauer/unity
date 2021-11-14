using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTests : MonoBehaviour
{
    UIPanel panel;
    const string Show = "Show";
    const string ShowRight = "ShowRight";
    const string ShowLeft = "ShowLeft";
    const string Hide = "Hide";
    const string Center = "Center";
    // Start is called before the first frame update
    void Start()
    {
        panel = GetComponent<UIPanel>();
        UIPanel.Position centerPos = new UIPanel.Position(Center, TextAnchor.MiddleCenter, TextAnchor.MiddleCenter);
        panel.AddPosition(centerPos);
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), ShowLeft))
        {
            Tweener t = panel.SetPosition(ShowLeft, true);
            t.easingControl.equation = EasingEquations.EaseInOutBack;
        }

        if (GUI.Button(new Rect(10, 50, 100, 30), Hide))
        {
            panel.SetPosition(Hide, true);
        }
        
        if (GUI.Button(new Rect(10, 90, 100, 30), ShowRight))
        {
            Tweener t = panel.SetPosition(ShowRight, true);
            t.easingControl.equation = EasingEquations.EaseInOutBack;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
