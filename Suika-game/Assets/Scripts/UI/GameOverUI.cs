using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    [SerializeField]
    TMP_Text finalScoreText;

    void Start()
    {
        if (panel != null)
            panel.SetActive(false);
        GameManager.Instance.OnGameOver += ShowPanel;
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnGameOver -= ShowPanel;
    }

    void ShowPanel()
    {
        panel.SetActive(true);
        finalScoreText.text = $"Score: {GameManager.Instance.Score}";
    }

    public void OnRestartButton()
    {
        GameManager.Instance.Restart();
    }
}
