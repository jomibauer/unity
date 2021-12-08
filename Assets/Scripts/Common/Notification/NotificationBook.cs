using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Remember to note the listener and poster of the notifications when you add one to the book.
public struct NotificationBook
{
    #region Conversation
        public static string CONVERSATION_COMPLETE = "CONVERSATION_COMPLETE"; // listener: ConversationController, observers managed by CutSceneState, poster: ConversationController
    #endregion

    #region Debug stats
        public static string DEBUG_ARRAY = "DEBUG_ARRAY"; // listener: SelectedUnitListener, poster: BattleController
        public static string SELECTED_UNIT = "SELECTED_UNIT"; // listener: SelectedUnitListener, poster: MoveTargetState
        public static string STATE_CHANGE = "STATE_CHANGE"; // listener: StateListener, poster: Observer base class, implemented & therefore called by every GameState
        public static string TILE_INFO = "TILE_INFO"; // listener: InfoImgListener & TextListener, poster: MoveTargetState
        public static string UNIT_INFO = "UNIT_INFO"; // listener: InfoImgListener & TextListener, poster: MoveTargetState
    #endregion

    #region Input
        public static string CANCEL = "CANCEL"; // listener: InputController, poster: base BattleState class
        public static string CONFIRM = "CONFIRM"; // listener: InputController, poster: base BattleState class
        public static string INFO = "INFO"; // listener: InputController, poster: base BattleState class
        public static string INPUT_OFF = "INPUT_OFF"; // listener: InputController, poster: base BattleState class, in Disable method
        public static string INPUT_ON = "INPUT_ON"; // listener: InputController, poster: Any Gamestate that requires input.
        public static string MOVE = "MOVE"; // listener: InputController, poster: base BattleState class
        public static string PAUSE = "PAUSE"; // listener: InputController, poster: base BattleState class
    #endregion

    #region Skirmish Play View
        public static string AWARD_EXP_FINISHED = "AWARD_EXP_FINISHED"; // listener: AwardExpState, poster: ExpBar 
        public static string AWARD_EXP_START = "AWARD_EXP_START"; // listener: ExpBar, poster: AwardExpState
        public static string SKIRMISH_START = "SKIRMISH_START"; // listener: SkirmishPlayViewController, poster: SkirmishPlayState  
        public static string HEALTH_CHANGE_FINISHED = "HEALTH_CHANGE_FINISHED";
        public static string HIDE_SKIRMISH_PLAY_VIEW = "HIDE_SKIRMISH_PLAY_VIEW"; // listener: SkirmishPlayViewController, poster:
        public static string LEFT_PANE_HEALTH_CHANGE = "LEFT_PANE_HEALTH_CHANGE"; // listener: SkirmishPlayViewController, poster:
        public static string PLAY_NEXT_ROUND = "PLAY_NEXT_ROUND"; // listener: SkirmishPlayState, poster: SkirmishPlayViewController
        public static string RIGHT_PANE_HEALTH_CHANGE = "RIGHT_PANE_HEALTH_CHANGE"; // listener: SkirmishPlayViewController, poster:
        public static string SHOW_SKIRMISH_PLAY_VIEW = "SHOW_SKIRMISH_PLAY_VIEW"; // listener: SkirmishPlayViewController, poster: SkirmishInitState
        public static string SKIRMISH_SETUP_COMPLETE = "SKIMRISH_SETUP_COMPLETE"; //listener: SkirmishInitState, poster: SkirmishPlayViewController
        public static string SKIRMISH_SET_EXP = "SKIRMISH_SET_EXP"; // listener: ExpBarPane, poster:
    #endregion

    #region Skirmish Preview
        public static string HIDE_SKIRMISH_PREVIEW_PANE = "HIDE_SKIRMISH_PREVIEW_PANE"; // listener: SkirmishPreviewPane, poster: SkirmishInitState
        public static string SHOW_SKIRMISH_PREVIEW_PANE = "SHOW_SKIRMISH_PREVIEW_PANE"; // listener: SkirmishPreviewPane, poster: TargetSelectionState
        public static string SKIRMISH_PREVIEW_PANE_POPULATE = "SKIRMISH_PREVIEW_PANE_POPULATE"; // listener: SkirmishPreviewPane, poster: TargetSelectionState
    #endregion 

    #region Traversal
        public static string FINISHED_MOVING = "FINISHED_MOVING"; // listener : TraversalState, poster: UnitMover
        public static string UNIT_TILE_UPDATE = "UNIT_TILE_UPDATE"; // listener: UnitController, poster: Unit
    #endregion

    #region MISC
    //could be useful in the future.
    public static string EQUIPPED_ITEM = "EQUIPPED_ITEM"; // listener: nothing yet, poster: Equipment base class
    public static string UNEQUIPPED_ITEM = "UNEQUIPPED_ITEM"; // listener: nothing yet, poster: Equipment base class

    #endregion

}
