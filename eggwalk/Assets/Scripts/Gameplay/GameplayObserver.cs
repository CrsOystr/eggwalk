using UnityEngine;
using System.Collections;

public class GameplayObserver : MonoBehaviour, Observer
{

    public void onNotify(GameEvent e)
    {
        GameEnumerations.EventCategory category = e.Category;
        switch (category)
        {
            case GameEnumerations.EventCategory.Player_IsHurt:
                {
                    PlayerMotor player = e.Entity.GetComponent<PlayerMotor>();
                    player.RecieveDamage(1);
                    break;
                }
            case GameEnumerations.EventCategory.Player_IsDead:
                {
                    break;
                }

        }
    }
}