using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText;

    void Start()
    {
        GameManager.Instance.OnScoreChanged += Refresh;
        Refresh(GameManager.Instance.Score);
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnScoreChanged -= Refresh;
    }

    void Refresh(int score) => scoreText.text = score.ToString();
}
