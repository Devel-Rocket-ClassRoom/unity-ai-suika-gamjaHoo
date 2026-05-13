using System;
using System.Collections.Generic;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    [SerializeField]
    float triggerDuration = 2f;

    readonly HashSet<Collider2D> _fruitsAbove = new();
    static readonly Predicate<Collider2D> IsDestroyed = c => c == null;
    float _timer;

    void OnEnable() => _timer = triggerDuration;

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
            _timer = triggerDuration;
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver)
            return;

        _fruitsAbove.RemoveWhere(IsDestroyed);

        if (_fruitsAbove.Count == 0)
            return;

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
            GameManager.Instance.TriggerGameOver();
    }
}
