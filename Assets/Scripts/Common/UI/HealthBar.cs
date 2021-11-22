using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject border;
    [SerializeField] GameObject health;
    [SerializeField] GameObject background;

    SpriteRenderer borderSprite;
    SpriteRenderer healthSprite;
    SpriteRenderer backgroundSprite;

    int maxHP;
    int currentHP;
    [SerializeField] public float healthMoveSpeed = .1f;

    //these values only work based on the current size of the sprite; if I want to change the size of the sprite, 
    //I'll have to change these too.
    float startingHealthUnit = 0.3f;
    float barHeight = 0.7f;
    float oneHealthUnitWidth = 0.2f;

    void Awake()
    {
        this.borderSprite = border.GetComponent<SpriteRenderer>();
        this.healthSprite = health.GetComponent<SpriteRenderer>();
        this.backgroundSprite = background.GetComponent<SpriteRenderer>();
    }

    public void Load(int maxHealth, int health)
    {
        maxHP = maxHealth;
        currentHP = health;
        this.borderSprite.size = new Vector2(startingHealthUnit + ((maxHealth - 1) * oneHealthUnitWidth), barHeight);
        this.healthSprite.size = new Vector2((startingHealthUnit-.01f) + ((health - 1) * oneHealthUnitWidth), barHeight);
        this.backgroundSprite.size = new Vector2((startingHealthUnit-.01f) + ((maxHealth - 1) * oneHealthUnitWidth), barHeight);
    }

    public void RaiseMaxBy(int value = 1)
    {
        maxHP += 1;
        this.borderSprite.size += new Vector2((value * oneHealthUnitWidth), 0f);
        this.backgroundSprite.size += new Vector2((value * oneHealthUnitWidth), 0f);
    }

    public void HealBy(int value = 1)
    {
        value = currentHP + value <= maxHP ? value : maxHP - currentHP;
        currentHP += value;

        Debug.LogWarning($"HealthBar: healSTART{currentHP}/ {maxHP}");
        StartCoroutine(IncrementHealthBy(value));
        Debug.LogWarning($"HealthBar: healEND{currentHP}/ {maxHP}");

    }

    public void DamageBy(int value = 1)
    {
        value = currentHP - value >= 0 ? value : currentHP;
        currentHP -= value;
        Debug.LogWarning($"HealthBar: damageSTART{currentHP}/ {maxHP}");

        StartCoroutine(DecrementHealthBy(value));
        Debug.LogWarning($"HealthBar: damageEND{currentHP}/ {maxHP}");

    }

    IEnumerator IncrementHealthBy(int value)
    {
        for (int i = 0; i < value; i++)
        {
            Debug.Log($"HealthBar: heal{currentHP}/ {maxHP}");
            yield return new WaitForSeconds(healthMoveSpeed);
            this.healthSprite.size += new Vector2((oneHealthUnitWidth), 0f);
        }
    }

    IEnumerator DecrementHealthBy(int value)
    {
        for (int i = 0; i < value; i++)
        {
            Debug.Log($"HealthBar: damage{currentHP}/ {maxHP}");
            yield return new WaitForSeconds(healthMoveSpeed);
            this.healthSprite.size -= new Vector2((oneHealthUnitWidth), 0f);
        }
    }
}
