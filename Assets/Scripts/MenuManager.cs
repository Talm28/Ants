using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start() {
        UpdateScore();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
        UpdateScore();
    }

    public void UpdateScore()
    {
        _scoreText.text = "High Score: " + PlayerPrefs.GetInt("Score", 0).ToString();
    }
}
