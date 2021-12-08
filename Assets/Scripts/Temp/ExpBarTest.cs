using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBarTest : MonoBehaviour
{
    [SerializeField] UIPanel panel;
    // Start is called before the first frame update
    void Start()
    {
        //panel = GetComponent<UIPanel>();
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Show"))
        {
            Tweener t = panel.SetPosition("Show", true);
            t.easingControl.equation = EasingEquations.EaseInOutBack;
        }

        if (GUI.Button(new Rect(10, 50, 100, 30), "Hide"))
        {
            Tweener t = panel.SetPosition("Hide", true);
            t.easingControl.equation = EasingEquations.EaseInOutBack;
        }

    }
}
