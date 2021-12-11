using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    TileConverter tileConverter;
    public void Awake()
    {
        tileConverter = GetComponent<TileConverter>();
    }
    public void InitUnit(Unit unit)
    {
        unit.tileConverter = tileConverter;

        unit.movePoint.parent = null;
        unit.path = new List<Tile>();

        unit.isActive = true;

        unit.stats = unit.GetComponent<Stats>();
        GameObject go = Resources.Load($"Units/{unit.unit_name}_stats") as GameObject;
        GameObject unitStatsObject = Instantiate(go, new Vector2(0,0), Quaternion.identity, unit.transform);
        unit.unitStats = unit.GetComponentInChildren<UnitStats>();

        unit.unitStats.LoadStats();
        unit.HP = unit.stats[StatTypes.MHP];

        unit.spriteRenderer = unit.GetComponent<SpriteRenderer>();
        Sprite map_sprite = Resources.Load<Sprite>($"Sprites/Units/Map/{unit.unitStats.color}{unit.unitStats.unit_class.dataName}");
        unit.spriteRenderer.sprite = map_sprite;
        Animator animator = unit.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load($"Animations/units/slimes/{unit.unitStats.color}{unit.unitStats.unit_class.dataName}") as RuntimeAnimatorController;
        unit.levelComponent = unit.GetComponent<LevelComponent>();
        unit.levelComponent.LoadLevelComponent(unit.unitStats.GetLoadExp(), unit.unitStats.GetLoadLevel(), unit.unitStats.unit_class.RNK);

        unit.weapon = unit.GetComponentInChildren<Weapon>();
        unit.inventory = unit.GetComponentInChildren<Inventory>();
        unit.inventory.Load(unit.unit_name);
    }
}
