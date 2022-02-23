using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapMenuController : BaseMenuController
{
    [SerializeField] UIPanel confirmPanel;
    [SerializeField] Text confirmTitle;
    [SerializeField] MenuEntry unitEntry;
    [SerializeField] MenuEntry optionsEntry;
    [SerializeField] MenuEntry objectivesEntry;
    [SerializeField] MenuEntry suspendEntry;
    [SerializeField] MenuEntry endEntry;
    [SerializeField] GameObject selectedBar;
    List<MenuEntry> defaultEntries;
    List<MenuEntry> confirmEntries;
    RectTransform selectTransform;
    [SerializeField] int lastSelection;
    protected override void Start()
    {
        base.Start();
        selectTransform = selectedBar.GetComponent<RectTransform>();
        defaultEntries = new List<MenuEntry> {unitEntry, optionsEntry, objectivesEntry, suspendEntry, endEntry};
        confirmEntries = confirmPanel.GetComponentsInChildren<MenuEntry>().ToList();
        lastSelection = -1;
        ToggleConfirm(HideKey);
        this.menuEntries.AddRange(defaultEntries);
    }

    protected override bool SetSelection(int value)
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
            MoveBar(selection);
            menuEntries[selection].IsSelected = true;
        }
      
        return true;
    }

    void MoveBar(int selection)
    {
         selectTransform.localPosition = new Vector2(-10 , (110 - (selection * 55)));
    }

    public void SwitchToConfirm(string title)
    {
        ResetSelection();
        selection = 0;
        this.selectedBar.SetActive(false);
        this.confirmTitle.text = title;
        this.menuEntries.Clear();
        this.menuEntries.AddRange(confirmEntries);
        ToggleConfirm(ShowKey);
    }

    public void SwitchToMain()
    {
        ResetSelection();
        // /selection = lastSelection;
        ToggleConfirm(HideKey);
        this.selectedBar.SetActive(true);
        this.menuEntries.Clear();
        this.menuEntries.AddRange(defaultEntries);
        // /selection = lastSelection;
    }

    public void ToggleConfirm(string pos)
    {
        Tweener t = confirmPanel.SetPosition(pos, false);
        //t.easingControl.duration = 0.5f;
        //t.easingControl.equation = EasingEquations.EaseOutQuad;
    }
    public void ShowConfirm()
    {
        confirmPanel.SetPosition(ShowKey, true);
    }
    public void HideConfirm()
    {
        confirmPanel.SetPosition(HideKey, false);
    }

    public override void HideMenu()
    {
        HideConfirm();
        base.HideMenu();
    }

    public override void ShowMenu()
    {
        selection = lastSelection;
        canvas.SetActive(true);
        TogglePos(ShowKey);
    }

    public void SetLastSelection(int lastSelection)
    {
        this.lastSelection = lastSelection;
    }

    public int GetSelection()
    {
        return selection;
    }

}
