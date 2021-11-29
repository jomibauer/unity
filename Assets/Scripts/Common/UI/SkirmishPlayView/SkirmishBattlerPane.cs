using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkirmishBattlerPane : MonoBehaviour
{
    [SerializeField] HealthBarPane healthBarPane;
    [SerializeField] NamePane namePane;
    [SerializeField] SkirmishStatsPane skirmishStatsPane;
    UIPanel panel;

    void Start()
    {
        this.panel = GetComponent<UIPanel>();
    }
    public void Load(SkirmishStatSet stats)
    {
        namePane.Load(stats.unit_name);
        skirmishStatsPane.Load(stats);
        healthBarPane.Load(stats.HP, stats.MaxHP);
    }

    public Tweener SetPosition(string pos)
    {
        return this.panel.SetPosition(pos, true);
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
