using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPane : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] Text numberRepresentation;
    [SerializeField] public float healthMoveSpeed = .1f;
    Image background;
    int currentHealth;
    int maxHealth;

    void Start()
    {
        background = GetComponent<Image>();
    }
    public void Clear()
    {
        this.currentHealth = 0;
        this.maxHealth = 0;
        this.numberRepresentation.text = "HP";
    }

    public void Load(int currentHP, int maxHP, Factions faction)
    {
        this.currentHealth = currentHP;
        this.maxHealth = maxHP;
        healthBar.Load(maxHP, currentHP);
        numberRepresentation.text = currentHealth.ToString();
        background.sprite = Resources.Load<Sprite>($"Sprites/UI/Skirmish/skirmish_play_{faction.ToString()}_health");
    }

    public IEnumerator IncrementHealthBy(int value)
    {
        value = currentHealth + value <= maxHealth ? value : maxHealth - currentHealth; 
        for (int i = 0; i < value; i++)
        {

            currentHealth = healthBar.Heal();
            numberRepresentation.text = currentHealth.ToString();
            yield return new WaitForSeconds(healthMoveSpeed);
        }
        
    }

    public IEnumerator DecrementHealthBy(int value)
    {
        value = currentHealth - value >= 0 ? value : currentHealth;
        for (int i = 0; i < value; i++)
        {

            currentHealth = healthBar.Damage();
            numberRepresentation.text = currentHealth.ToString();
            yield return new WaitForSeconds(healthMoveSpeed);
        }
    }

    public void RaiseMaxHealth()
    {
        healthBar.RaiseMaxBy();
    }

    
}
