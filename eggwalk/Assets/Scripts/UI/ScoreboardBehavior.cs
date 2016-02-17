using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardBehavior : MonoBehaviour {

    [SerializeField] int _numOfScores;
    [SerializeField] PlayerPrefsManager _playerPrefsManager;

    private int[] _scores;

	void OnEnable()
    {
        // TODO: add the ability to actually record eggs delivered score
        _scores = _playerPrefsManager.GetEggsDeliveredScore(SceneManager.GetActiveScene().name, _numOfScores);
        for(int i = 0; i < _scores.Length; i++)
        {
            Debug.Log("Eggs Delivered score: " + _scores[i]);
        }
    }
	
}
