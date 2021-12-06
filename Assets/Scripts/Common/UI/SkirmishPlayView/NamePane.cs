using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamePane : MonoBehaviour
{
    [SerializeField] Text nameField;
    Image background;

    void Start()
    {
        background = GetComponent<Image>();
    }
    public void Load(string name, Factions faction)
    {
        nameField.text = name;
        background.sprite = Resources.Load<Sprite>($"Sprites/UI/Skirmish/skirmish_play_{faction.ToString()}_name");
    }

    public void Clear()
    {
        nameField.text = "";
    }
}
