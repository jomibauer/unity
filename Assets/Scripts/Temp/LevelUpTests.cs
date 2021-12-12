using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpTests : MonoBehaviour
{
   [SerializeField] BattleController controller;
   [SerializeField] Unit expRecipent;

   void OnGUI()
   {
      if (GUI.Button(new Rect(10, 10, 100, 30), "Do Skirmish"))
      {
         controller.ChangeState<TestSkirmishInitState>();
      }
      if (GUI.Button(new Rect(10, 50, 100, 30), "ReadyToGiveExp"))
      {
         controller.ChangeState<TestAwardExpState>();
      }
      if (GUI.Button(new Rect(10, 90, 100, 30), "Show LevelUp Pane"))
      {
         this.PostNotification("TEST_SHOW_LEVEL", 30);
      }

      if (GUI.Button(new Rect(10, 130, 100, 30), "Hide LevelUp Pane"))
      {
         this.PostNotification("TEST_HIDE_LEVEL", 30);
      }

      if (GUI.Button(new Rect(150, 10, 100, 30), "Award 30"))
      {
         this.PostNotification("TEST_AWARD_EXP", 30);
      }
      if (GUI.Button(new Rect(150, 50, 100, 30), "Award 101"))
      {
         this.PostNotification("TEST_AWARD_EXP", 101);
      }

        
   }
}
