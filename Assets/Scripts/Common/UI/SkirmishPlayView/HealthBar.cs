using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject border;
    [SerializeField] GameObject health;
    [SerializeField] GameObject background;

    RectTransform borderRect;
    RectTransform healthRect;
    RectTransform backgroundRect;

    int maxHP;
    int currentHP;
    [SerializeField] public float healthMoveSpeed = .1f;

    //these values only work based on the current size of the sprite; if I want to change the size of the sprite, 
    //I'll have to change these too.
    float startingHealthUnit = 30f;
    float barHeight = 70f;
    float oneHealthUnitWidth = 20f;

    void Awake()
    {
        this.borderRect = border.GetComponent<RectTransform>();
        this.healthRect = health.GetComponent<RectTransform>();
        this.backgroundRect = background.GetComponent<RectTransform>();
    }

    public void Load(int maxHealth, int health)
    {
        maxHP = maxHealth;
        currentHP = health;
        this.borderRect.sizeDelta = new Vector2(startingHealthUnit + ((maxHealth - 1) * oneHealthUnitWidth), barHeight);
        this.healthRect.sizeDelta = new Vector2((startingHealthUnit-1f) + ((health - 1) * oneHealthUnitWidth), barHeight);
        this.backgroundRect.sizeDelta = new Vector2((startingHealthUnit-1f) + ((maxHealth - 1) * oneHealthUnitWidth), barHeight);
    }

    public void RaiseMaxBy(int value = 1)
    {
        maxHP += 1;
        this.borderRect.sizeDelta += new Vector2((value * oneHealthUnitWidth), 0f);
        this.backgroundRect.sizeDelta += new Vector2((value * oneHealthUnitWidth), 0f);
    }

    public int Heal()
    {
        
        currentHP += 1;
        this.healthRect.sizeDelta += new Vector2((oneHealthUnitWidth), 0f);
        return currentHP;

    }

    public int Damage()
    {
        Debug.LogWarning($"HealthBar: damageSTART{currentHP}/ {maxHP}");
        currentHP -= 1;
        this.healthRect.sizeDelta -= new Vector2((oneHealthUnitWidth), 0f);
        Debug.LogWarning($"HealthBar: damageEND{currentHP}/ {maxHP}");
        return currentHP;
       

    }

    IEnumerator IncrementHealthBy(int value)
    {
        for (int i = 0; i < value; i++)
        {
            Debug.Log($"HealthBar: heal{currentHP}/ {maxHP}");
            yield return new WaitForSeconds(healthMoveSpeed);
            this.healthRect.sizeDelta += new Vector2((oneHealthUnitWidth), 0f);
        }
    }

    IEnumerator DecrementHealthBy(int value)
    {
        for (int i = 0; i < value; i++)
        {
            Debug.Log($"HealthBar: damage{currentHP}/ {maxHP}");
            yield return new WaitForSeconds(healthMoveSpeed);
            this.healthRect.sizeDelta -= new Vector2((oneHealthUnitWidth), 0f);
        }
    }
}
