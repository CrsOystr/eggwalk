using UnityEngine;
using System.Collections.Generic;

public class UIObserver : MonoBehaviour, Observer
{
    [SerializeField] private GameState gameState;
    [SerializeField] private UISystem UISys;

	private PlayerPrefsManager ppm = new PlayerPrefsManager();

    public void onNotify(GameEvent e)
    {
        GameEnumerations.EventCategory category = e.Category;

        switch (category)
        {
            case GameEnumerations.EventCategory.Gameplay_InitializeHUD:
                {
                    PlayerMotor player = e.Entity[0].GetComponent<PlayerMotor>();
                    UISys.setCurrentLives(player.getCurrentLives(), player.getTotalLives());
                    UISys.setCountDownText(gameState.InitialTimeToStart + "");
					UISys.setScoreText(gameState.Score);
                    break;
                }
            case GameEnumerations.EventCategory.Gameplay_InitializeEvents:
                {
                    List<Objective> objList = gameState.getObjectiveList();
                    UISys.setObjectiveListText(objList);

                    break;
                }
            case GameEnumerations.EventCategory.Gameplay_Countdown:
                {
                    if (gameState.CountdownTime > 0)
                    {
                        UISys.setCountDownText(gameState.CountdownTime + "");
                    } else if (gameState.CountdownTime == 0)
                    {
                        UISys.setCountDownText("GO!");
                    } else
                    {
                        UISys.setCountDownText("");
                    }

                    break;
                }
            case GameEnumerations.EventCategory.Gameplay_StartLevel:
                {
                    break;
                }
            case GameEnumerations.EventCategory.Gameplay_CompletedLevel:
                {
                    UISys.goToNextLevelScreen();
                    UISys.setTimeText(gameState.TimeInLevel);

					/*
					 * RECORD LEVEL SCORE
					 * 
					 * There's a possibility that in the future we might only use one scene (since each level takes place in the same city, right?), and the different objects would be loaded 
					 * dynamically based on the level the player selected, then we might need some unique level ID rather than just calling 'Application.loadedLevelName'
					 */
					ppm.addTimeScore(Application.loadedLevelName, gameState.TimeInLevel);

                    break;
                }
            case GameEnumerations.EventCategory.Player_IsHurt:
                {
                    PlayerMotor playerCharacter = e.Entity[0].GetComponent<PlayerMotor>();
                    if (playerCharacter.getPlayerAlive())
                    {
                        int currentLives = playerCharacter.getCurrentLives();
                        int totalLives = playerCharacter.getTotalLives();
                        UISys.setCurrentLives(currentLives, totalLives);
                        UISys.showHurtMask(true);
                    }

                    break;
                }
            case GameEnumerations.EventCategory.Player_IsDead:
                {
                    UISys.goToGameOverScreen();
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
                    UISys.setVisibilityToBalanceBar(true);

                    break;
                }
            case GameEnumerations.EventCategory.Player_ReturnedTarget:
                {
                    PlayerMotor player = e.Entity[0].GetComponent<PlayerMotor>();
                    GameObject obj = gameState.getCurrentObjective();
                    GameObject pickup = player.getItemInHand();
                    GameObject collider = e.Entity[1];
                    List<Objective> objList = gameState.getObjectiveList();
					this.UISys.setScoreText(gameState.Score);
                    if (obj != null)
                    {
                        UISys.setObjectiveListText(obj.GetComponent<Objective>(), objList);
                    } else
                    {
                        UISys.setDeliveredText(gameState.getLastDeliveredItem());
                        UISys.setVisiblilityToDeliveredText(true);
                        UISys.setObjectiveListText(objList);
                    }

                    if (collider.GetComponent<TriggerBox>().isTargetEqual(pickup))
                    {
                        UISys.setVisibilityToBalanceBar(false);
                    }
                    break;
                }
        }
    }
}
