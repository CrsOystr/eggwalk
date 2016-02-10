using UnityEngine;
using System.Collections;

public class GameplayObserver : MonoBehaviour, Observer
{
    [SerializeField]
    private GameState gameState;

    public void onNotify(GameEvent e)
    {
        GameEnumerations.EventCategory category = e.Category;
        switch (category)
        {
            case GameEnumerations.EventCategory.Gameplay_InitializeEvents:
                {
                    gameState.initializeLevel();
                    break;
                }
            case GameEnumerations.EventCategory.Gameplay_StartLevel:
                {
                    gameState.startGame();
                    break;
                }
            case GameEnumerations.EventCategory.Gameplay_CompletedLevel:
                {

                    break;
                }
            case GameEnumerations.EventCategory.Player_HasRotatedHands:
                {
                    PlayerMotor player = e.Entity[0].GetComponent<PlayerMotor>();
                    if (player == null)
                    {
                        break;
                    }

                    Objective obj = gameState.getCurrentObjective().GetComponent<Objective>();
                    if (obj != null)
                    {
                        Pickup p = obj.getObjectiveItem().GetComponent<Pickup>();
                        if (p != null)
                        {
                            p.onRotateAction(player.getRollingRotation);
                        }
                    }

                    break;
                }
            case GameEnumerations.EventCategory.Player_IsHurt:
                {
                    PlayerMotor player = e.Entity[0].GetComponent<PlayerMotor>();
                    player.bumpPlayer();
                    player.RecieveDamage(1);
                    break;
                }
            case GameEnumerations.EventCategory.Player_IsDead:
                {
                    gameState.IsGameOver = true;

                    PlayerMotor player = e.Entity[0].GetComponent<PlayerMotor>();
                    player.onDeath();

                    GameObject pickup = player.getItemInHand();
                    if (pickup != null)
                    {
                        pickup.GetComponent<Pickup>().onDropAction();
                    }
                    break;
                }
            case GameEnumerations.EventCategory.Player_StartedObjective:
                {
                    PlayerMotor player = e.Entity[0].GetComponent<PlayerMotor>();

                    Pickup pickup = e.Entity[1].GetComponent<Pickup>();
                    pickup.onPickupAction(player.ItemLocation);

                    gameState.startObjective(pickup.getId());

                    Transform targetDestination = gameState.getDestinationFromObjective(pickup.getId());

                    if (targetDestination != null)
                    {
                        player.enableArrow(true);
                        player.setTarget(targetDestination);

                        player.setTarget(targetDestination);
                        player.addItemIntoHand(e.Entity[1]);
                    }

                    break;
                }
            case GameEnumerations.EventCategory.Player_ReturnedTarget:
                {
                    PlayerMotor player = e.Entity[0].GetComponent<PlayerMotor>();
                    GameObject p = player.getItemInHand();
                    Pickup pickup = p.GetComponent<Pickup>();

                    // Reset player to not contain any items
                    player.returnToNeutral();
                    player.removeItemFromHand();
                    Destroy(p);

                    // Affect game state by adding to a score and adding
                    // to a collection of returned items by the player
                    gameState.addToScore(pickup.getScoreValue());
                    gameState.addToItemDeliveredList(pickup.getName());
                    gameState.completeObjective(pickup.getId());

                    if (gameState.getGameMode().ResetLivesAfterObjectiveComplete)
                    {
                        player.resetLives();
                    }

                    if (gameState.getGameMode().IsEndless)
                    {
                        gameState.restartEndlessObjective(e.Entity[0]);
                    }

                    break;
                }

        }
    }

    private void handleReturnedTarget(GameEvent e)
    {
        PlayerMotor player = e.Entity[0].GetComponent<PlayerMotor>();
        GameObject p = player.getItemInHand();

        TriggerBox trigger = e.Entity[1].GetComponent<TriggerBox>();
        if (!trigger.isTargetEqual(p))
        {
            return;
        }

        Pickup pickup = p.GetComponent<Pickup>();
        Objective obj = gameState.getObjectiveFromId(pickup.getId());
        GameEnumerations.ObjectiveType objType = obj.getObjectiveType();

        switch (objType)
        {
            case GameEnumerations.ObjectiveType.Objective_Return:
                {
                    player.returnToNeutral();
                    player.removeItemFromHand();
                    Destroy(p);

                    gameState.addToScore(pickup.getScoreValue());
                    //gameState.restartObjective(obj.getObjectiveID());
                    gameState.addToItemDeliveredList(pickup.getName());
                    break;
                }
            case GameEnumerations.ObjectiveType.Objective_Collect:
                {
                    break;
                }
            case GameEnumerations.ObjectiveType.Objective_Search_Return:
                {

                    break;
                }
        }
    }
}