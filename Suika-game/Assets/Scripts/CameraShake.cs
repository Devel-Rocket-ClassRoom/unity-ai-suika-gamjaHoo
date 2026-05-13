using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Shake(float duration = 0.2f, float magnitude = 0.1f)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        var cam = Camera.main;
        if (cam == null)
            yield break;

        var origin = cam.transform.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float strength = magnitude * (1f - elapsed / duration);
            cam.transform.localPosition = origin + (Vector3)(Random.insideUnitCircle * strength);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cam.transform.localPosition = origin;
    }
}
