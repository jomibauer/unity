using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarTests : MonoBehaviour
{
    float x = -9f;
    float y = 4.5f;

    int count =0;
    GameObject barPrefab;
    GameObject testBar;
    HealthBar testHealth;

    // Start is called before the first frame update
    void Start()
    {
        barPrefab = Resources.Load("Prefabs/UI/HealthBar") as GameObject;
        testBar = Instantiate(barPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        testHealth= testBar.GetComponent<HealthBar>();
        testHealth.Load(3, 3);
        
    }

    void CreateHealthBar(int currentHP, int maxHP)
    {
        GameObject healthBar = Instantiate(barPrefab, new Vector3(x, y, 0), Quaternion.identity);
        HealthBar health = healthBar.GetComponent<HealthBar>();
        health.Load(maxHP, currentHP);
        y -= .5f;
        count += 1;
        /* GameObject text = new GameObject("box");
        text.transform.position = new Vector3(x, y - .25f, 0);
        text.AddComponent<Text>();
        Text texttext = text.GetComponent<Text>();
        texttext.text = $"{currentHP}/{maxHP}"; */
        if (count % 18 == 0)
        {
            x -= 5;
            y = 4.5f;
        }
    }

    void Update()
    {
        if(Input.GetButtonUp("Pause"))
        {
            //TestBar();
            TestRaiseMax();
        }
        if(Input.GetButtonUp("Confirm"))
        {
            Debug.Log("Tests: Conf");
            TestHeal();
        }
        if(Input.GetButtonUp("Cancel"))
        {
            Debug.Log("Tests: Cancel");
            TestDamage();
        }
    }

    private void TestRaiseMax()
    {
        testHealth.RaiseMaxBy();
    }

    private void TestDamage()
    {
        testHealth.DamageBy(3);
    }

    private void TestHeal()
    {
        testHealth.HealBy(5);
    }

    void TestBar()
    {
        CreateHealthBar(15, 15);
        CreateHealthBar(15, 20);
        CreateHealthBar(30, 30);
        CreateHealthBar(1, 1);
        CreateHealthBar(1, 30);
        CreateHealthBar(7, 10);
        CreateHealthBar(5, 5);
        CreateHealthBar(18, 24);
        CreateHealthBar(19, 19);
        CreateHealthBar(4, 21);
        CreateHealthBar(3, 32);
        CreateHealthBar(12, 26);
        CreateHealthBar(11, 11);
        CreateHealthBar(15, 17);
    }
}
