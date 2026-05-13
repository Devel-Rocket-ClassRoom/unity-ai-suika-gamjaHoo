using System.Collections;
using TMPro;
using UnityEngine;

public class FirstMergePopup : MonoBehaviour
{
    [SerializeField]
    TMP_Text popupText;

    RectTransform _textRT;

    void Start()
    {
        if (popupText != null)
        {
            _textRT = popupText.GetComponent<RectTransform>();
            popupText.alpha = 0f;
        }
        GameManager.Instance.OnFirstMerge += ShowPopup;
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnFirstMerge -= ShowPopup;
    }

    void ShowPopup() => StartCoroutine(PopupRoutine());

    IEnumerator PopupRoutine()
    {
        if (popupText == null)
            yield break;

        float duration = 1.5f;
        float elapsed = 0f;
        var startPos = _textRT.anchoredPosition;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float alpha = t < 0.2f ? t / 0.2f : 1f - (t - 0.2f) / 0.8f;
            popupText.alpha = alpha;
            _textRT.anchoredPosition = startPos + Vector2.up * (t * 120f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        popupText.alpha = 0f;
        _textRT.anchoredPosition = startPos;
    }
}
