using System.Collections.Generic;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    [SerializeField]
    float triggerDuration = 2f;

    [SerializeField]
    float restVelocityThreshold = 0.3f;

    readonly HashSet<Collider2D> _fruitsAbove = new();
    float _timer;
    bool _counting;

    void OnTriggerEnter2D(Collider2D col)
    {
        var fruit = col.GetComponent<Fruit>();
        if (fruit == null || fruit.IsPreview)
            return;
        _fruitsAbove.Add(col);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        _fruitsAbove.Remove(col);
        if (_fruitsAbove.Count == 0)
            ResetTimer();
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver)
            return;

        // 파괴된 오브젝트 제거
        _fruitsAbove.RemoveWhere(c => c == null);

        bool anyResting = false;
        foreach (var col in _fruitsAbove)
        {
            var rb = col.GetComponent<Rigidbody2D>();
            if (rb != null && rb.linearVelocity.magnitude <= restVelocityThreshold)
            {
                anyResting = true;
                break;
            }
        }

        if (anyResting)
        {
            if (!_counting)
            {
                _counting = true;
                _timer = triggerDuration;
            }
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
                GameManager.Instance.TriggerGameOver();
        }
        else
        {
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        _counting = false;
        _timer = triggerDuration;
    }
}
