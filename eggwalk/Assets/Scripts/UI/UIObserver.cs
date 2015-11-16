using UnityEngine;
using System.Collections.Generic;

public class UIObserver : MonoBehaviour, Observer
{
    [SerializeField]
    private UISystem UISys;

    public void onNotify(GameEvent e)
    {
        GameEnumerations.EventCategory category = e.Category;

        switch (category)
        {
            case GameEnumerations.EventCategory.Gameplay_InitializeEvents:
                {
                    List<GameObject> objectives = e.Entity;
                    string text = "";
                    for (int i = 0; i < objectives.Count; i++)
                    {
                        text += "Obj " + (i + 1) + ": ";
                        text += objectives[i].GetComponent<Objective>().getObjectiveName();
                        text += "\n";
                    }

                    UISys.setObjectiveListText(text);
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
        }
    }
}
