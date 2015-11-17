using UnityEngine;
using System.Collections.Generic;

namespace GameEnumerations {
	public enum EventCategory {
		Player_PastRotationBounds,
		Player_IsHurt,
		Player_IsDead,
        Player_HasRotatedHands,
		Player_CanTurnRight,
		Player_CanTurnLeft,
		Player_IsTurningLeft,
        Player_IsTurningRight,
        Player_StartedObjective,
        Player_ReturnedTarget,
        Gameplay_InitializeEvents,
        Gameplay_InitializeHUD
    };
}

public class GameEvent {
	public List<GameObject> Entity;
	public GameEnumerations.EventCategory Category;

	public GameEvent(List<GameObject> Entity, GameEnumerations.EventCategory e) {
		this.Entity = Entity;
		this.Category = e;
	}
}