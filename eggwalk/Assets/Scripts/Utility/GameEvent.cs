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
        Player_TargetApproaching,
        Player_TargetLeaving,
        Gameplay_InitializeEvents,
        Gameplay_InitializeHUD,
        Gameplay_Countdown,
        Gameplay_StartLevel,
        Gameplay_Paused,
        Gameplay_CompletedLevel
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