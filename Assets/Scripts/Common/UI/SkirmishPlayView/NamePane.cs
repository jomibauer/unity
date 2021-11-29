using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePane : MonoBehaviour
{
    [SerializeField] Text nameField;


    public void Load(string name)
    {
        nameField.text = name;
    }

    public void Clear()
    {
        nameField.text = "";
    }
}
