using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private ScoreController _scoreController;


    private void OnEnable() {
        int score = _scoreController.Score;
        _scoreText.text = $"Score: {score}";
        if(score > PlayerPrefs.GetInt("Score", 0))
            PlayerPrefs.SetInt("Score", score);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
