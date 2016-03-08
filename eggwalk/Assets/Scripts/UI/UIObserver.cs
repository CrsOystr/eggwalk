using UnityEngine;
using UnityEngine.SceneManagement;
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
                    UISys.setCountDownText(gameState.TimeToStart + "");
					UISys.setScoreText(gameState.Score);
                    UISys.activateWarningLabel(false);
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
                    if (gameState.TimeToStart > 0)
                    {
                        UISys.setCountDownText(gameState.TimeToStart + "");
                    } else if (gameState.TimeToStart == 0)
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
            case GameEnumerations.EventCategory.Gameplay_Paused:
                {
                    UISys.goToPauseScreen();
                    break;
                }
            case GameEnumerations.EventCategory.Gameplay_CompletedLevel:
                {
                    UISys.goToNextLevelScreen();
                    UISys.setTimeText(gameState.TimeInLevel);

                    

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
                    UISys.goToGameOverScreen(gameState.Score);

                    /*
					 * RECORD LEVEL SCORE
					 */
                    ppm.SetEggsDeliveredScore(ppm.LastEggInstantiated.index, ppm.LastEggInstantiated.name, gameState.Score);
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
            case GameEnumerations.EventCategory.Player_TargetApproaching:
                {
                    GameObject player = e.Entity[0];
                    GameObject vehicle = e.Entity[1];

                    if (Vector3.Dot(player.transform.forward, vehicle.transform.forward) > 0.97f)
                    {
                        UISys.activateWarningLabel(true);
                    }
                    break;
                }
            case GameEnumerations.EventCategory.Player_TargetLeaving:
                {
                    UISys.activateWarningLabel(false);
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
                    UISys.setDeliveredText(gameState.getLastDeliveredItem());

                    if (obj != null)
                    {
                        UISys.setObjectiveListText(obj.GetComponent<Objective>(), objList);
                        UISys.setVisiblilityToDeliveredText(true);
                    } else
                    {
                        UISys.setObjectiveListText(objList);
                    }

                    if (collider.GetComponent<TriggerBox>().isTargetEqual(pickup))
                    {
                        UISys.setVisibilityToBalanceBar(false);
                    }

                    if (this.gameState.getGameMode().ResetLivesAfterObjectiveComplete)
                    {
                        UISys.setCurrentLives(player.getCurrentLives(), player.getTotalLives());
                    }

                    break;
                }
        }
    }
}
