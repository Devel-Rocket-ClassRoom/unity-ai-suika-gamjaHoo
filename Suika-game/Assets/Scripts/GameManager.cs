using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Score { get; private set; }
    public bool IsGameOver { get; private set; }
    public bool HasFirstMerged { get; private set; }

    public event Action<int> OnScoreChanged;
    public event Action OnGameOver;
    public event Action OnFirstMerge;
    public event Action OnWatermelonSpawned;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddScore(int points)
    {
        if (IsGameOver)
            return;
        Score += points;
        OnScoreChanged?.Invoke(Score);
    }

    public void TriggerGameOver()
    {
        if (IsGameOver)
            return;
        IsGameOver = true;
        OnGameOver?.Invoke();
    }

    public void TryFireFirstMerge()
    {
        if (HasFirstMerged)
            return;
        HasFirstMerged = true;
        OnFirstMerge?.Invoke();
    }

    public void TryFireWatermelonSpawned() => OnWatermelonSpawned?.Invoke();

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
