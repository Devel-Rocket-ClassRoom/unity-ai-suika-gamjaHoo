using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    Image _image;

    void Awake()
    {
        var canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 200;
        gameObject.AddComponent<CanvasScaler>();
        gameObject.AddComponent<GraphicRaycaster>();

        var imgGo = new GameObject("FlashOverlay");
        imgGo.transform.SetParent(canvas.transform, false);
        _image = imgGo.AddComponent<Image>();
        _image.color = new Color(1f, 1f, 1f, 0f);
        _image.raycastTarget = false;
        var rt = imgGo.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = rt.offsetMax = Vector2.zero;
    }

    void Start()
    {
        GameManager.Instance.OnWatermelonSpawned += OnWatermelon;
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnWatermelonSpawned -= OnWatermelon;
    }

    void OnWatermelon() => StartCoroutine(FlashRoutine(new Color(1f, 0.9f, 0.2f)));

    IEnumerator FlashRoutine(Color flashColor)
    {
        float duration = 0.5f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float alpha = t < 0.25f ? t / 0.25f : (1f - t) / 0.75f;
            _image.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha * 0.65f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _image.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
    }
}
