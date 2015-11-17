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
                    player.RecieveDamage(1);
                    break;
                }
            case GameEnumerations.EventCategory.Player_IsDead:
                {
                    break;
                }
            case GameEnumerations.EventCategory.Player_StartedObjective:
                {
                    Objective obj = e.Entity[1].GetComponent<Objective>();
                    Pickup p = e.Entity[2].GetComponent<Pickup>();

                    gameState.startObjective(obj.getObjectiveID());
                    break;
                }
            case GameEnumerations.EventCategory.Player_ReturnedTarget:
                {
                    PlayerMotor player = e.Entity[0].GetComponent<PlayerMotor>();
                    GameObject pickup = player.getItemInHand();
                    if (e.Entity[1].GetComponent<TriggerBox>().isTargetEqual(pickup))
                    {
                        player.returnToNeutral();
                        Destroy(pickup);
                    }
                    break;
                }

        }
    }
}