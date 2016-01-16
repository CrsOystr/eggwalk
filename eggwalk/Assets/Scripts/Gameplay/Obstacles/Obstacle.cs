using UnityEngine;
using System.Collections;

namespace GameEnumerations
{
    public enum ObstacleType
    {
        Obstacle_Generic,
        Obstacle_Vehicle
    }
}

public interface Obstacle {
    void onObstacleCollision();
    bool hasCollided();
    GameEnumerations.ObstacleType getObstacleCategory();
}