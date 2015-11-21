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
            case GameEnumerations.EventCategory.Player_IsHurt:
                {
                    PlayerMotor player = e.Entity[0].GetComponent<PlayerMotor>();
                    player.bumpPlayer();
                    player.RecieveDamage(1);
                    break;
                }
            case GameEnumerations.EventCategory.Player_IsDead:
                {
                    Pickup pickup = e.Entity[0].GetComponent<PlayerMotor>().getItemInHand().GetComponent<Pickup>();
                    pickup.pickupAction();
                    break;
                }
            case GameEnumerations.EventCategory.Player_StartedObjective:
                {
                    Objective obj = e.Entity[1].GetComponent<Objective>();
                    gameState.startObjective(obj.getObjectiveID());
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

                        gameState.completeObjective(obj.getObjectiveID());
                        gameState.addToItemDeliveredList(pickup.GetComponent<Pickup>().getName());
                        player.returnToNeutral();
                        Destroy(pickup);
                    }
                    break;
                }

        }
    }
}