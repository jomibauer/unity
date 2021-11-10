using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedUnitListener : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        this.AddObserver(OnUnitSelect, "UNIT_MOVEMENT");

    }

    private void OnUnitSelect(object sender, object info)
    {
        List<string> ls = (List<string>)info;
        string joined = string.Join("\n", ls.ToArray());
        text.text = joined;
    }
}
