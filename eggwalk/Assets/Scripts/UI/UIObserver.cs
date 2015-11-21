using UnityEngine;
using System.Collections.Generic;

public class UIObserver : MonoBehaviour, Observer
{
    [SerializeField] private GameState gameState;
    [SerializeField] private UISystem UISys;

    public void onNotify(GameEvent e)
    {
        GameEnumerations.EventCategory category = e.Category;

        switch (category)
        {
            case GameEnumerations.EventCategory.Gameplay_InitializeHUD:
                {
                    PlayerMotor player = e.Entity[0].GetComponent<PlayerMotor>();
                    UISys.setCurrentLives(player.getCurrentLives(), player.getTotalLives());
                    break;
                }
            case GameEnumerations.EventCategory.Gameplay_InitializeEvents:
                {
                    List<Objective> objList = gameState.getObjectiveList();
                    UISys.setObjectiveListText(objList);

                    break;
                }
            case GameEnumerations.EventCategory.Player_IsHurt:
                {
                    PlayerMotor playerCharacter = e.Entity[0].GetComponent<PlayerMotor>();
                    int currentLives = playerCharacter.getCurrentLives();
                    int totalLives = playerCharacter.getTotalLives();
                    UISys.setCurrentLives(currentLives, totalLives);
                    UISys.showHurtMask(true);

                    break;
                }
            case GameEnumerations.EventCategory.Player_IsDead:
                {
                    UISys.setVisibilityToRestart(true);
                    UISys.setVisibilityToLives(false);
                    break;
                }
            case GameEnumerations.EventCategory.Player_HasRotatedHands:
                {
                    PlayerMotor playerCharacter = e.Entity[0].GetComponent<PlayerMotor>();
                    float rotation = playerCharacter.getRollingRotation;
                    UISys.setRotationToBalanceBar(rotation);
                    break;
                }
            case GameEnumerations.EventCategory.Player_CanTurnLeft:
                {
                    UISys.showLeftSignalImage(true);
                    break;
                }
            case GameEnumerations.EventCategory.Player_CanTurnRight:
                {
                    UISys.showRightSignalImage(true);
                    break;
                }
            case GameEnumerations.EventCategory.Player_IsTurningLeft:
                {
                    UISys.showRightSignalImage(false);
                    break;
                }
            case GameEnumerations.EventCategory.Player_IsTurningRight:
                {
                    UISys.showLeftSignalImage(false);
                    break;
                }
            case GameEnumerations.EventCategory.Player_StartedObjective:
                {
                    Objective obj = gameState.getCurrentObjective().GetComponent<Objective>();
                    List<Objective> objList = gameState.getObjectiveList();
                    UISys.setObjectiveListText(obj, objList);

                    break;
                }
            case GameEnumerations.EventCategory.Player_ReturnedTarget:
                {
                    GameObject obj = gameState.getCurrentObjective();
                    List<Objective> objList = gameState.getObjectiveList();
                    if (obj != null)
                    {
                        UISys.setObjectiveListText(obj.GetComponent<Objective>(), objList);
                    } else
                    {
                        UISys.setDeliveredText(gameState.getLastDeliveredItem());
                        UISys.setVisiblilityToDeliveredText(true);
                        UISys.setObjectiveListText(objList);
                    }
                    break;
                }
        }
    }
}
