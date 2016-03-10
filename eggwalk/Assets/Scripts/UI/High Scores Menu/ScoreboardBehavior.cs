using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreboardBehavior : MonoBehaviour {

    [SerializeField] private PlayerPrefsManager _playerPrefsManager;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private ScorePanelBehavior _scorePanelPrefab;
    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private float _scorePanelSpacing;

    private List<ScorePanelBehavior> _instantiatedScorePanels;

    public void LoadScoresForLevel(string levelName)
    {
        StartCoroutine(DelayedLoadScoresForLevel(levelName));
    }

    private IEnumerator DelayedLoadScoresForLevel(string levelName)
    {
        yield return new WaitForSeconds(0.5f);

        if (_hintPanel.gameObject.activeInHierarchy) _hintPanel.SetActive(false);

        // clean up list of instantiated ScorePanels
        if (_instantiatedScorePanels != null)
        {
            foreach (ScorePanelBehavior obj in _instantiatedScorePanels)
            {
                Destroy(obj.gameObject);
            }
        }
        _instantiatedScorePanels = new List<ScorePanelBehavior>();
        
        List<int> scoreList = _playerPrefsManager.GetEggsDeliveredScores(levelName);

        for (int i = 0; i < scoreList.Count; i++)
        {
            ScorePanelBehavior newScorePanel = Instantiate(_scorePanelPrefab).GetComponent<ScorePanelBehavior>();
            newScorePanel.scoreText.text = "#" + (i + 1) + ": " + scoreList[i];
            newScorePanel.transform.SetParent(transform);
            newScorePanel.transform.Rotate(new Vector3(0, 180, 0));
            newScorePanel.transform.localScale = Vector3.one;
            newScorePanel.transform.position = new Vector3(
                0,
                0 - (i * _scorePanelSpacing) + 8f,
                0);

            _instantiatedScorePanels.Add(newScorePanel);
        }

        GetComponent<RectTransform>().sizeDelta = new Vector2(-50, scoreList.Count * _scorePanelSpacing + 300f);

        StartCoroutine(ScrollToTop());
    }

    public void StartScrollToTop()
    {
        StartCoroutine(ScrollToTop());
    }

    private IEnumerator ScrollToTop()
    {
        yield return null;

        _scrollRect.verticalScrollbar.value = 1f;
    }

}
