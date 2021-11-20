using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NotificationBook
{
    //Remember to note the listener and poster of the notifications when you add one to the book.
    //public static string AWARD_EXP = "AWARD_EXP"; // listener: ExperienceManager, poster: AwardExpState
    public static string CANCEL = "CANCEL"; // listener: InputController, poster: base BattleState class
    public static string CONFIRM = "CONFIRM"; // listener: InputController, poster: base BattleState class
    public static string CONVERSATION_COMPLETE = "CONVERSATION_COMPLETE"; // listener: ConversationController, observers managed by CutSceneState, poster: ConversationController
    public static string DEBUG_ARRAY = "DEBUG_ARRAY"; // listener: SelectedUnitListener, poster: BattleController
    public static string HIDE_SKIRMISH_PREVIEW_PANE = "HIDE_SKIRMISH_PREVIEW_PANE";
    public static string INFO = "INFO"; // listener: InputController, poster: base BattleState class
    public static string MOVE = "MOVE"; // listener: InputController, poster: base BattleState class
    public static string PAUSE = "PAUSE"; // listener: InputController, poster: base BattleState class
    public static string SELECTED_UNIT = "SELECTED_UNIT"; // listener: SelectedUnitListener, poster: ??
    public static string SHOW_SKIRMISH_PREVIEW_PANE = "SHOW_SKIRMISH_PREVIEW_PANE"; // listener: SkirmishPreviewPane, poster: TargetSelectionState
    public static string SKIRMISH_PANE_POPULATE = "SKIRMISH_PANE_POPULATE"; // listener: SkirmishPreviewPane, poster: TargetSelectionState
    public static string SKIRMISH_START = "SKIRMISH_START"; // listener: SkirmishController, poster: ??
    public static string STATE_CHANGE = "STATE_CHANGE"; // listener: StateListener, poster: Observer base class, implemented & therefore called by every GameState
    public static string TILE_INFO = "TILE_INFO"; // listener: InfoImgListener & TextListener, poster: ??
    public static string UNIT_INFO = "UNIT_INFO"; // listener: InfoImgListener & TextListener, poster: ??
    public static string EQUIPPED_ITEM = "EQUIPPED_ITEM"; // listener: nothing yet, poster: Equipment base class
    public static string UNEQUIPPED_ITEM = "UNEQUIPPED_ITEM"; // listener: nothing yet, poster: Equipment base class

    

}
