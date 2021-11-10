using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoImgListener : MonoBehaviour
{
    Image image;
    [SerializeField]string path;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
        this.AddObserver(OnUnitInfo, "UNIT_INFO");
        this.AddObserver(OnTileInfo, "TILE_INFO");
    }

    private void OnTileInfo(object sender, object t)
    {
        image.enabled = false;
    }

    private void OnUnitInfo(object sender, object u)
    {
        image.enabled=true;
        Unit unit = (Unit)u;
        path = $"Sprites/Units/InfoPane/{unit.data.unit_class}";
        image.sprite = (Sprite)Resources.Load<Sprite>(path);
    }
}
