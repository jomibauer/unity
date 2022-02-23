using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMenuTest : MonoBehaviour
{
    [SerializeField] MapMenuController controller;
    void Start()
    {
        controller.ShowMenu();
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Show"))
        {
            controller.ToggleConfirm("Show");
        }

        if (GUI.Button(new Rect(10, 50, 100, 30), "Hide"))
        {
            controller.ToggleConfirm("Hide");
        }

    }
}
