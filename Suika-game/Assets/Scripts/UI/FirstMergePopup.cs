using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstMergePopup : MonoBehaviour
{
    TextMeshProUGUI _text;
    RectTransform _textRT;

    void Awake()
    {
        var canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 150;
        gameObject.AddComponent<CanvasScaler>();
        gameObject.AddComponent<GraphicRaycaster>();

        var textGo = new GameObject("PopupText");
        textGo.transform.SetParent(canvas.transform, false);
        _text = textGo.AddComponent<TextMeshProUGUI>();
        _text.text = "NICE!";
        _text.fontSize = 72;
        _text.fontStyle = FontStyles.Bold;
        _text.alignment = TextAlignmentOptions.Center;
        _text.color = new Color(1f, 0.85f, 0f, 0f);
        _textRT = _text.GetComponent<RectTransform>();
        _textRT.anchorMin = _textRT.anchorMax = new Vector2(0.5f, 0.4f);
        _textRT.sizeDelta = new Vector2(400f, 100f);
        _textRT.anchoredPosition = Vector2.zero;
    }

    void Start()
    {
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
        float duration = 1.5f;
        float elapsed = 0f;
        var startPos = Vector2.zero;
        _textRT.anchoredPosition = startPos;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float alpha = t < 0.2f ? t / 0.2f : 1f - (t - 0.2f) / 0.8f;
            _text.color = new Color(1f, 0.85f, 0f, alpha);
            _textRT.anchoredPosition = startPos + Vector2.up * (t * 120f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _text.color = new Color(1f, 0.85f, 0f, 0f);
        _textRT.anchoredPosition = startPos;
    }
}
