using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMenuTest : MonoBehaviour
{
    AbilityMenuPanelController abilityMenuPanelController;
    // Start is called before the first frame update
    void Start()
    {
        this.abilityMenuPanelController = GetComponent<AbilityMenuPanelController>();
        abilityMenuPanelController.Show("Attack", new List<string> {"run", "go", "park"});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
