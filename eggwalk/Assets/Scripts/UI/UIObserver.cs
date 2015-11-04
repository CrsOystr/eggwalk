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
                    PlayerMotor playerCharacter = e.Entity.GetComponent<PlayerMotor>();
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
                    PlayerMotor playerCharacter = e.Entity.GetComponent<PlayerMotor>();
                    float rotation = playerCharacter.getRollingRotation;
                    UISys.setRotationToBalanceBar(rotation);
                    break;
                }
        }
    }
}
