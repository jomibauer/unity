using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkirmishStatsPane : MonoBehaviour
{
    [SerializeField] Text hit;
    [SerializeField] Text damage;
    [SerializeField] Text crit;

    public void Load(SkirmishStatSet stats)
    {
        hit.text = stats.hit.ToString();
        damage.text = stats.dam.ToString();
        crit.text = stats.dam.ToString();
    }

    public void Clear()
    {
        hit.text = "";
        damage.text = "";
        crit.text = "";
    }
}
