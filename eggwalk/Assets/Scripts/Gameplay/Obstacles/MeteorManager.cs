using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeteorManager : MonoBehaviour, Observer
{
    public GameState gameState;
    public List<Meteor> meteors;
    public List<int> scoreRequired;
    private int currentMeteor = 0;

    public void onNotify(GameEvent e)
    {
        GameEnumerations.EventCategory category = e.Category;

        switch (category)
        {
            case GameEnumerations.EventCategory.Player_ReturnedTarget:
                {
                    for (int i = 0; i < scoreRequired.Count; i++)
                    {
                        if (gameState.Score == scoreRequired[i])
                        {
                            LaunchMeteor();
                        }
                    }

                    break;
                }
        }
    }

    public void LaunchMeteor()
    {
        if (currentMeteor < meteors.Count)
        {
            meteors[currentMeteor].Launch();
            currentMeteor++;
        }
    }
}
