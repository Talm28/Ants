using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private int _antScore;
    [SerializeField] private int _speedyAntscore;
    [SerializeField] private int _angryAntScore;

    public int Score {get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        UpdateScore();
    }

    public void AddScore(int score)
    {
        Score += score;
        UpdateScore();
    }
    public void AddScore(GameObject gameObject)
    {
        switch(gameObject.tag)
        {
            case "Ant":
                Score += _antScore;
                break;
            case "Speedy ant":
                Score += _speedyAntscore;
                break;
            case "Angry ant":
                Score += _angryAntScore;
                break;
        }
        UpdateScore();
    }

    private void UpdateScore()
    {
        _scoreText.text = Score.ToString();
    }
}
