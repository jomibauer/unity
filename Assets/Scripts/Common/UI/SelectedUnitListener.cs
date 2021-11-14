using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedUnitListener : MonoBehaviour
{
    Text text;
    //This is a leftover class that is kinda useful for general debugging.  Should rename this class, but I am not doing that rn.
    void Start()
    {
        text = GetComponent<Text>();
        this.AddObserver(OnUnitSelect, NotificationBook.DEBUG_ARRAY);

    }

    private void OnUnitSelect(object sender, object info)
    {
        List<string> ls = (List<string>)info;
        string joined = string.Join("\n", ls.ToArray());
        text.text = joined;
    }
}
