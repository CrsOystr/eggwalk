using UnityEngine;
using System.Collections;

public class SelectLevelButton : MonoBehaviour {

	[SerializeField] private string _levelName;

	public string LevelName
	{
		get { return _levelName; }
	}
}
