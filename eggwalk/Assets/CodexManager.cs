using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class CodexManager : MonoBehaviour {

    [SerializeField] private EggCodexUIElement _eggUIPrefab;
    [SerializeField] private PlayerPrefsManager _playerPrefsManager;
    [SerializeField] private float _rowSpacing;
    [SerializeField] private float _columnSpacing;
    [SerializeField] private int _eggsPerRow = 2;

    private List<EggCodexUIElement> _eggUIElements;
    private RectTransform _thisRect;
    private ScrollRect _scrollRect;
    private bool _hasBeenLoaded;

    void Start()
    {

    }

    void OnEnable()
    {
        if (_hasBeenLoaded) return;

        _eggUIElements = new List<EggCodexUIElement>();
        RectTransform rectTransform = _eggUIPrefab.GetComponent<RectTransform>();

        List<PlayerPrefsManager.EggData> eggList = _playerPrefsManager.AllEggsInGame;

        for (int i = 0; i < eggList.Count; i++)
        {
            Vector3 rectPos = new Vector3(
                (i % _eggsPerRow) * (rectTransform.rect.width + _columnSpacing) + rectTransform.rect.width / 2, // place along x axis
                -Mathf.Floor((i) / _eggsPerRow) * (rectTransform.rect.height + _rowSpacing) - rectTransform.rect.height / 2, // place along y axis
                0f);
            EggCodexUIElement _eggUIElement = Instantiate(_eggUIPrefab, rectPos, Quaternion.identity) as EggCodexUIElement;

            if (eggList[i].hasBeenSuccesfullyDelivered)
            {
                _eggUIElement.eggName.text = eggList[i].name;
                _eggUIElement.eggSprite.enabled = false;

                GameObject egg = Instantiate(_playerPrefsManager.LoadEggWithName(eggList[i].name), _eggUIElement.transform.position, Quaternion.identity) as GameObject;
                egg.transform.SetParent(_eggUIElement.transform);
                egg.layer = LayerMask.NameToLayer("UI");
                egg.GetComponent<ExplodePickup>().enabled = false;
                egg.transform.GetChild(0).gameObject.SetActive(false);
                egg.transform.localScale = new Vector3(10f, 10f, 10f);
                _eggUIElement.eggObject = egg;
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

        _hasBeenLoaded = true;

        //StartCoroutine(ScrollToTop());
        _scrollRect.verticalNormalizedPosition = 1.0f;
    }

    private IEnumerator ScrollToTop()
    {
        //Time.timeScale = 0;
        yield return new WaitForSeconds(1.0f);
        _scrollRect.normalizedPosition = new Vector2(0, 1);
    }
}
