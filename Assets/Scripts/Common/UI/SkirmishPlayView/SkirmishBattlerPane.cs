using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkirmishBattlerPane : MonoBehaviour
{
    [SerializeField] HealthBarPane healthBarPane;
    [SerializeField] NamePane namePane;
    [SerializeField] SkirmishStatsPane skirmishStatsPane;
    UIPanel panel;

    //Note: for this to load properly, you need sprites saved following a certain convention in the correct place.
    // in Resources/Sprites/UI/Skirmish/ you need the following png files:
    // - skirmish_play_{faction}_health
    // - skirmish_play_{faction}_name
    // with one file for each faction from the Factions enum.
    void Start()
    {
        this.panel = GetComponent<UIPanel>();
    }
    public void Load(SkirmishStatSet stats)
    {
        namePane.Load(stats.unit_name, stats.faction);
        skirmishStatsPane.Load(stats);
        healthBarPane.Load(stats.HP, stats.MaxHP, stats.faction);
    }

    public Tweener SetPosition(string pos, bool animate=true)
    {
        return this.panel.SetPosition(pos, animate);
    }
    public IEnumerator DamageBy(int amount)
    {
        yield return StartCoroutine(healthBarPane.DecrementHealthBy(amount));
        yield return new WaitForSeconds(1);
        this.PostNotification(NotificationBook.HEALTH_CHANGE_FINISHED);
    }

    public IEnumerator HealBy(int amount)
    {
        yield return StartCoroutine(healthBarPane.IncrementHealthBy(amount));
        yield return new WaitForSeconds(1);
        this.PostNotification(NotificationBook.HEALTH_CHANGE_FINISHED);
    }

    public void RaiseMaxHealth()
    {
        healthBarPane.RaiseMaxHealth();
    }

    public void Clear()
    {
        namePane.Clear();
        skirmishStatsPane.Clear();
        healthBarPane.Clear();
    }
    /* public void TEST_SetHealth(int hp, int max_hp)
    {
        healthBarPane.Load(hp, max_hp);
    }
    public void TEST_SetName(string name)
    {
        namePane.Load(name);
    }
    public void TEST_SetStats(int hit, int dam, int crit, Unit unit)
    {
        SkirmishStatSet stats = new SkirmishStatSet(hit, dam, crit, 1, unit);
        skirmishStatsPane.Load(stats);
    }

    public void TEST_Damage(int damage)
    {
        StartCoroutine(healthBarPane.DecrementHealthBy(damage));
    } */
}
