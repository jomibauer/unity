using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEntry : MonoBehaviour
{
    [SerializeField] Text label;
    
    public string Title
    {
        get { return label.text; }
        set { label.text = value; }
    }

    [System.Flags]
    enum States
    {
        None=0,
        Selected = 1 << 0,
        Locked = 1 << 1
    }
    public bool IsLocked
    {
      get { return (State & States.Locked) != States.None; }
      set
      {
        if (value)
          State |= States.Locked;
        else
          State &= ~States.Locked;
      }
    }
    public bool IsSelected
    {
      get { return (State & States.Selected) != States.None; }
      set
      {
        if (value)
          State |= States.Selected;
        else
          State &= ~States.Selected;
      }
    }
    States State
    { 
      get { return state; }
      set
      {
        if (state == value)
          return;
        state = value;
        
        if (IsLocked)
        {
          
          label.color = Color.gray;
          
        }
        else if (IsSelected)
        {
          
          label.color = new Color32(249, 210, 118, 255);
          
        }
        else
        {
          
          label.color = Color.white;
          
        }
      }
    }
    States state;

    public void Reset ()
    {
      State = States.None;
    }

}
