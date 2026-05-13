using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    [SerializeField]
    Image flashImage;

    void Start()
    {
        if (flashImage != null)
            flashImage.color = new Color(1f, 0.9f, 0.2f, 0f);
        GameManager.Instance.OnWatermelonSpawned += OnWatermelon;
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnWatermelonSpawned -= OnWatermelon;
    }

    void OnWatermelon() => StartCoroutine(FlashRoutine());

    IEnumerator FlashRoutine()
    {
        if (flashImage == null)
            yield break;

        var flashColor = new Color(1f, 0.9f, 0.2f);
        float duration = 0.5f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float alpha = t < 0.25f ? t / 0.25f : (1f - t) / 0.75f;
            flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha * 0.65f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
    }
}
