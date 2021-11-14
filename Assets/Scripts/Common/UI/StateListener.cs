using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateListener : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        this.AddObserver(OnStateChange, NotificationBook.STATE_CHANGE);

    }

    private void OnStateChange(object sender, object info)
    {
        text.text = info.ToString();
    }
}
