using UnityEngine;

namespace GameEnumerations {
	public enum EventCategory {
		Player_PastRotationBounds,
		Player_IsHurt,
		Player_IsDead,
        Player_HasRotatedHands
	};
}

public class GameEvent {
	public GameObject Entity;
	public GameEnumerations.EventCategory Category;

	public GameEvent(GameObject Entity, GameEnumerations.EventCategory e) {
		this.Entity = Entity;
		this.Category = e;
	}
}