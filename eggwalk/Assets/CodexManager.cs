using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class CodexManager : MonoBehaviour {

    [SerializeField] private GameObject _eggUIPrefab;
    [SerializeField] private int _totalNumberOfEggs;
    [SerializeField] private float _rowSpacing;
    [SerializeField] private float _columnSpacing;
    [SerializeField] private int _eggsPerRow;

    private PlayerPrefsManager _playerPrefsManager;
    private List<GameObject> _eggUIElements;
    private RectTransform _thisRect;
    private ScrollRect _scrollRect;

    void OnEnable()
    {
        _eggUIElements = new List<GameObject>();
        RectTransform rectTransform = _eggUIPrefab.GetComponent<RectTransform>();
        for (int i = 0; i < _totalNumberOfEggs; i++)
        {
            Vector3 rectPos = new Vector3(
                (i % _eggsPerRow) * (rectTransform.rect.width + _columnSpacing) + rectTransform.rect.width/2, // place along x axis
                -Mathf.Floor((i) / _eggsPerRow) * (rectTransform.rect.height + _rowSpacing) - rectTransform.rect.height/2, // place along y axis
                0f);
            _eggUIElements.Add(Instantiate(_eggUIPrefab, rectPos, Quaternion.identity) as GameObject);
            _eggUIElements[i].transform.SetParent(transform, false);
        }

        _thisRect = GetComponent<RectTransform>();
        _thisRect.sizeDelta = new Vector2(-50, _eggUIPrefab.GetComponent<RectTransform>().rect.height * (_totalNumberOfEggs / _eggsPerRow));

        _scrollRect = GetComponentInParent<ScrollRect>();

        StartCoroutine(ScrollToTop());

    }

    private IEnumerator ScrollToTop()
    {
        yield return null;

        _scrollRect.verticalScrollbar.value = 1f;
    }
}
