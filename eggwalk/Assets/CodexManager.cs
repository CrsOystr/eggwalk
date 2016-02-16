using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class CodexManager : MonoBehaviour {

    [SerializeField] private GameObject _eggUIPrefab;
    [SerializeField] private PlayerPrefsManager _playerPrefsManager;
    [SerializeField] private float _rowSpacing;
    [SerializeField] private float _columnSpacing;
    [SerializeField] private int _eggsPerRow;

    private List<GameObject> _eggUIElements;
    private RectTransform _thisRect;
    private ScrollRect _scrollRect;

    void OnEnable()
    {
        _eggUIElements = new List<GameObject>();
        RectTransform rectTransform = _eggUIPrefab.GetComponent<RectTransform>();

        List<PlayerPrefsManager.EggData> eggList = _playerPrefsManager.AllEggsInGame;

        for (int i = 0; i < eggList.Count; i++)
        {
            Vector3 rectPos = new Vector3(
                (i % _eggsPerRow) * (rectTransform.rect.width + _columnSpacing) + rectTransform.rect.width / 2, // place along x axis
                -Mathf.Floor((i) / _eggsPerRow) * (rectTransform.rect.height + _rowSpacing) - rectTransform.rect.height / 2, // place along y axis
                0f);
            GameObject _eggUIElement = Instantiate(_eggUIPrefab, rectPos, Quaternion.identity) as GameObject;

            if (eggList[i].hasBeenSuccesfullyDelivered)
            {
                _eggUIElement.GetComponentInChildren<Text>().text = eggList[i].name;
                foreach (Transform t in transform)
                {
                    if (t.name == "Egg Sprite")
                    {
                        t.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                _eggUIElement.GetComponentInChildren<Text>().text = "???";
            }

            _eggUIElement.transform.SetParent(transform, false);
            _eggUIElements.Add(_eggUIElement);
        }

        _thisRect = GetComponent<RectTransform>();
        _thisRect.sizeDelta = new Vector2(-50, _eggUIPrefab.GetComponent<RectTransform>().rect.height * (eggList.Count / _eggsPerRow));

        _scrollRect = GetComponentInParent<ScrollRect>();

        StartCoroutine(ScrollToTop());

    }

    private IEnumerator ScrollToTop()
    {
        yield return null;

        _scrollRect.verticalScrollbar.value = 1f;
    }
}
