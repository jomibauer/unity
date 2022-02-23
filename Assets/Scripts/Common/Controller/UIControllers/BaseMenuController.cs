using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenuController : MonoBehaviour
{
    public const string HideKey = "Hide";
    public const string ShowKey = "Show";
    public List<MenuEntry> menuEntries;
    protected int selection { get; set; }

    [SerializeField] public UIPanel panel;
    [SerializeField] public GameObject canvas;

    protected virtual void Start() 
    {
        panel.SetPosition(HideKey, false);
        canvas.SetActive(false);
    }

    protected virtual bool SetSelection (int value)
    {
        if (menuEntries[value].IsLocked)
        {
            return false;
        }
      
        // Deselect the previously selected entry
        if (selection >= 0 && selection < menuEntries.Count)
        {
            menuEntries[selection].IsSelected = false;
        }
      
        selection = value;
      
        // Select the new entry
        if (selection >= 0 && selection < menuEntries.Count)
        {
            menuEntries[selection].IsSelected = true;
        }
      
        return true;
    }

    public virtual void Next ()
    {
        Debug.Log($"[BaseMenuController]: {selection}");
        for (int i = selection + 1; i < selection + menuEntries.Count; ++i)
        {
            int index = i % menuEntries.Count;
            if (SetSelection(index)){ break; }
        }
    }
    public virtual void Previous ()
    {
        for (int i = selection - 1 + menuEntries.Count; i > selection; --i)
        {
            int index = i % menuEntries.Count;
            if (SetSelection(index)){ break; }
        }
    }

    public virtual void ResetSelection()
    {
        menuEntries.ForEach(me => me.IsSelected = false);
    }

    protected virtual Tweener TogglePos (string pos)
    {
        Tweener t = panel.SetPosition(pos, true);
        t.easingControl.duration = 0.5f;
        t.easingControl.equation = EasingEquations.EaseOutQuad;
        return t;
    }

   public virtual void ShowMenu()
   {
       selection = -1;
       canvas.SetActive(true);
       TogglePos(ShowKey);
   }
   public virtual void HideMenu()
   {
       Tweener t = TogglePos(HideKey);
        t.easingControl.completedEvent += delegate(object sender, System.EventArgs e) 
        {
            if (panel.CurrentPosition == panel[HideKey])
            {
                canvas.SetActive(false);
            }
        };
   }



}