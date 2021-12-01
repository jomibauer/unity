using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarTests : MonoBehaviour
{
    float x = -9f;
    float y = 4.5f;

    int count = 0;
    GameObject barPrefab;
    GameObject testBar;
    HealthBar testHealth;
    const string Show = "Show";
    const string Hide = "Hide";
    [SerializeField] Unit unit;
    [SerializeField] SkirmishPlayViewController skirmishStatsViewController;
    SkirmishBattlerPane rightPane;
    SkirmishBattlerPane leftPane;

    // Start is called before the first frame update
    void Start()
    {
        barPrefab = Resources.Load("Prefabs/UI/HealthBar") as GameObject;
        /* testBar = Instantiate(barPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        testHealth= testBar.GetComponent<HealthBar>();
        testHealth.Load(3, 3); */
        rightPane = skirmishStatsViewController.rightPane;
        leftPane = skirmishStatsViewController.leftPane;
/*         rightPane.TEST_SetName("Blobby");
        leftPane.TEST_SetName("Norridge");
        rightPane.TEST_SetStats(97, 10, 13, unit);
        leftPane.TEST_SetStats(85, 16, 23, unit);
         */
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
        /* if(Input.GetButtonUp("Confirm"))
        {
            Debug.Log("Tests: Conf");
            TestHeal();
        }
        if(Input.GetButtonUp("Cancel"))
        {
            Debug.Log("Tests: Cancel");
            TestDamage();
        } */
    }

    private void TestRaiseMax()
    {
        testHealth.RaiseMaxBy();
    }

    private void TestDamage()
    {
        testHealth.Damage();
    }

    private void TestHeal()
    {
        testHealth.Heal();
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

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "ShowLeft"))
        {
            Tweener t = leftPane.SetPosition(Show);
            t.easingControl.equation = EasingEquations.EaseInOutBack;
        }

        if (GUI.Button(new Rect(10, 50, 100, 30), Hide))
        {
            Tweener t1 = leftPane.SetPosition(Hide);
            Tweener t2 = rightPane.SetPosition(Hide);
            t1.easingControl.equation = EasingEquations.EaseInOutBack;
            t2.easingControl.equation = EasingEquations.EaseInOutBack;
        }
        
        if (GUI.Button(new Rect(10, 90, 100, 30), "ShowRight"))
        {
            Tweener t = rightPane.SetPosition(Show);
            t.easingControl.equation = EasingEquations.EaseInOutBack;
        }

        if (GUI.Button(new Rect(200, 90, 100, 30), "Show"))
        {
            Tweener t1 = leftPane.SetPosition(Show);
            Tweener t2 = rightPane.SetPosition(Show);
            t1.easingControl.equation = EasingEquations.EaseInOutBack;
            t2.easingControl.equation = EasingEquations.EaseInOutBack;
        }

        if (GUI.Button(new Rect(200, 10, 100, 30), "AddHP"))
        {
            int rand1 = UnityEngine.Random.Range(1, 30);
            int actual1 = UnityEngine.Random.Range(1, rand1+1);
            int rand2 = UnityEngine.Random.Range(1, 30);
            int actual2 = UnityEngine.Random.Range(1, rand2+1);

/*             leftPane.TEST_SetHealth(actual1, rand1);
            rightPane.TEST_SetHealth(actual2, rand2); */
        }

        if (GUI.Button(new Rect(200, 50, 100, 30), "Damage"))
        {
/*             leftPane.TEST_Damage(5);
            rightPane.TEST_Damage(5) */;
        }
    }
}
