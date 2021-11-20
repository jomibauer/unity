using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Repeater
{
    const float threshold = 0.5f;
    const float rate = 0.25f;
    float _next;
    bool _hold;
    string _axis;
    public Repeater (string axisName)
    {
    _axis = axisName;
    }
    public int Update ()
    {
    int retValue = 0;
    int value = Mathf.RoundToInt( Input.GetAxisRaw(_axis) );
    if (value != 0)
    {
        if (Time.time > _next)
        {
        retValue = value;
        _next = Time.time + (_hold ? rate : threshold);
        _hold = true;
        }
    }
    else
    {
        _hold = false;
        _next = 0;
    }
    return retValue;
    }
}
public class InputController : MonoBehaviour
{
    Repeater _hor = new Repeater("Horizontal");
    Repeater _ver = new Repeater("Vertical");

    // Update is called once per frame
    void Update()
    {
        int x = _hor.Update();
        int y = _ver.Update();
        if(x != 0 || y!= 0)
        {
            this.PostNotification(NotificationBook.MOVE, new Tile(x, y));
        }
        if(Input.GetButtonUp("Confirm"))
        {
            //Debug.Log("confirm");
            this.PostNotification(NotificationBook.CONFIRM, new Tile(x, y));
        }

        if(Input.GetButtonUp("Info"))
        {
           this.PostNotification(NotificationBook.INFO, new Tile(x, y));
        }

        if(Input.GetButtonUp("Cancel"))
        {
            this.PostNotification(NotificationBook.CANCEL, new Tile(x, y));
        }

        if(Input.GetButtonUp("Pause"))
        {
            this.PostNotification(NotificationBook.PAUSE, new Tile(x, y));
        }
    }
}
