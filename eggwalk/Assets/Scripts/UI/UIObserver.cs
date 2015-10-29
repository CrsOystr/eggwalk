using UnityEngine;
using System.Collections;

public class UIObserver : MonoBehaviour, Observer
{
    [SerializeField]
    private UISystem UISys;

    public void onNotify(GameEvent e)
    {
        GameEnumerations.EventCategory category = e.Category;

        switch (category)
        {
            case GameEnumerations.EventCategory.Player_IsHurt:
                {
                    PlayerCharacter playerCharacter = e.Entity.GetComponent<PlayerCharacter>();
                    int currentLives = playerCharacter.P_CurrentLives;
                    int totalLives = playerCharacter.P_TotalLives;

                    if (currentLives < 0)
                    {
                        UISys.setCurrentLives(0, totalLives);
                    }
                    else
                    {
                        UISys.setCurrentLives(currentLives, totalLives);
                    }

                    break;
                }
            case GameEnumerations.EventCategory.Player_IsDead:
                {
                    UISys.setVisibilityToRestart(true);
                    break;
                }
            case GameEnumerations.EventCategory.Player_HasRotatedHands:
                {
                    PlayerCharacter playerCharacter = e.Entity.GetComponent<PlayerCharacter>();
                    float rotation = playerCharacter.P_RollingRotation;
                    UISys.setRotationToBalanceBar(rotation);
                    break;
                }
        }
    }
}
