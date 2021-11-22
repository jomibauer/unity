using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneState : BattleState 
{
    ConversationController conversationController;
    ConversationData data;
    protected override void Awake ()
    {
        base.Awake ();
        conversationController = owner.GetComponentInChildren<ConversationController>();
        data = Resources.Load<ConversationData>("Conversations/IntroScene");
    }
    protected override void OnDestroy ()
    {
        base.OnDestroy ();
        if (data)
        {
            Resources.UnloadAsset(data);
        }
    }
    public override void Enable ()
    {
        base.Enable ();
        conversationController.Show(data);
        this.PostNotification(NotificationBook.INPUT_ON);
    }
    protected override void AddObservers ()
    {
        base.AddObservers ();
        conversationController.AddObserver(OnCompleteConversation, NotificationBook.CONVERSATION_COMPLETE);
    }
    protected override void RemoveObservers ()
    {
        base.RemoveObservers ();
        conversationController.RemoveObserver(OnCompleteConversation, NotificationBook.CONVERSATION_COMPLETE);
    }
    protected override void OnConfirm (object sender, object e)
    {
        base.OnConfirm (sender, e);
        conversationController.Next();
        //Debug.Log("Cutscene can hear");
    }
    void OnCompleteConversation (object sender, object e)
    {
        owner.ChangeState<MoveTargetState>();
    }
}