using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class CodexManager : MonoBehaviour {

    [SerializeField] private GameObject _eggUIPrefab;
    [SerializeField] private float _rowSpacing;
    [SerializeField] private float _columnSpacing;
    [SerializeField] private int _eggsPerRow;
    [SerializeField] private PlayerPrefsManager _playerPrefsManager;
    
    private List<GameObject> _eggUIElements;
    private RectTransform _thisRect;
    private ScrollRect _scrollRect;

    void OnEnable()
    {
        _eggUIElements = new List<GameObject>();
        RectTransform rectTransform = _eggUIPrefab.GetComponent<RectTransform>();
        for (int i = 0; i < _playerPrefsManager.AllEggsInGame.Count; i++)
        {
            Vector3 rectPos = new Vector3(
                (i % _eggsPerRow) * (rectTransform.rect.width + _columnSpacing) + rectTransform.rect.width/2, // place along x axis
                -Mathf.Floor((i) / _eggsPerRow) * (rectTransform.rect.height + _rowSpacing) - rectTransform.rect.height/2, // place along y axis
                0f);

            GameObject eggUIElement = Instantiate(_eggUIPrefab, rectPos, Quaternion.identity) as GameObject;
            eggUIElement.transform.SetParent(transform, false);
            if(_playerPrefsManager.AllEggsInGame[i].hasBeenSuccesfullyDelivered)
            {
                eggUIElement.GetComponentInChildren<Text>().text = _playerPrefsManager.AllEggsInGame[i].name;
            }
            else
            {
                eggUIElement.GetComponentInChildren<Text>().text = "???";
            }

            _eggUIElements.Add(eggUIElement);
        }

        _thisRect = GetComponent<RectTransform>();
        _thisRect.sizeDelta = new Vector2(-50, _eggUIPrefab.GetComponent<RectTransform>().rect.height * (_playerPrefsManager.AllEggsInGame.Count / _eggsPerRow));

        _scrollRect = GetComponentInParent<ScrollRect>();

        StartCoroutine(ScrollToTop());

    }

    private IEnumerator ScrollToTop()
    {
        yield return null;

        _scrollRect.verticalScrollbar.value = 1f;
    }
}
