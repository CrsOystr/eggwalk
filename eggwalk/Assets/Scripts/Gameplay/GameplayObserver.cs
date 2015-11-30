using UnityEngine;
using System.Collections;

public class GameplayObserver : MonoBehaviour, Observer
{
    [SerializeField] private GameState gameState;

    public void onNotify(GameEvent e)
    {
        GameEnumerations.EventCategory category = e.Category;
        switch (category)
        {
            case GameEnumerations.EventCategory.Gameplay_StartLevel:
                {
                    gameState.startGame();
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
                    pickup.onPickupAction();

                    gameState.startObjective(pickup.getId());

                    Transform targetDestination = gameState.getDestinationFromObjective(pickup.getId());

                    if (targetDestination != null)
                    {
                        player.enableArrow(true);
                        player.setTarget(targetDestination);
                    }

                    break;
                }
            case GameEnumerations.EventCategory.Player_ReturnedTarget:
                {
                    PlayerMotor player = e.Entity[0].GetComponent<PlayerMotor>();
                    GameObject pickup = player.getItemInHand();
                    GameObject collider = e.Entity[1];
                    if (collider.GetComponent<TriggerBox>().isTargetEqual(pickup))
                    {
                        Objective obj = collider.GetComponentInParent<Objective>();

                        player.enableArrow(false);
                        gameState.completeObjective(obj.getObjectiveID());
                        gameState.addToItemDeliveredList(pickup.GetComponent<Pickup>().getName());
                        player.returnToNeutral();
                        Destroy(pickup);

                        if (gameState.HasCompletedLevel)
                        {
                            gameState.completeLevel();
                            player.startPlayer(false);
                        }
                    }
                    break;
                }

        }
    }
}