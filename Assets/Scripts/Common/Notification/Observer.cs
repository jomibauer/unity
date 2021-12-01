using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    void Start()
    {

    }
    public virtual void Enable ()
    {
        this.PostNotification(NotificationBook.STATE_CHANGE, this.GetType());
        AddObservers();
    }
    
    public virtual void Disable ()
    {
        this.PostNotification(NotificationBook.INPUT_OFF);
        RemoveObservers();
    }

    protected virtual void OnDestroy ()
    {
        RemoveObservers();
    }

    protected virtual void AddObservers ()
    {
    }

    protected virtual void RemoveObservers ()
    {
    }

    /* protected virtual void OnMove(object sender, object info)
    {

    }

    protected virtual void OnConfirm(object sender, object info)
    {

    }

    protected virtual void OnCancel(object sender, object info)
    {

    }

    protected virtual void OnInfo(object sender, object info)
    {

    }

    protected virtual void OnPause(object sender, object info)
    {

    } */
}
