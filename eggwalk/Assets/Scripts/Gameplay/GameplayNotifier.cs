using UnityEngine;
using System.Collections.Generic;

public class GameplayNotifier : MonoBehaviour, Subject {
	[SerializeField] private List<GameObject> S_GameObjectObservers;
	private List<Observer> S_Observers = new List<Observer>();

	public void attach(Observer o) {
		S_Observers.Add (o);
	}

	public void detach(Observer o) {
		S_Observers.Remove (o);
	}

	public void notify(GameEvent e) {
		for (int i = 0; i < S_GameObjectObservers.Count; i++) {
			S_GameObjectObservers[i].GetComponent<Observer>().onNotify(e);
		}
	}
}