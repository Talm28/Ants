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

    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        UpdateScore();
    }

    public void AddScore(int score)
    {
        _score += score;
        UpdateScore();
    }
    public void AddScore(GameObject gameObject)
    {
        switch(gameObject.tag)
        {
            case "Ant":
                _score += _antScore;
                break;
            case "Speedy ant":
                _score += _speedyAntscore;
                break;
            case "Angry ant":
                _score += _angryAntScore;
                break;
        }
        UpdateScore();
    }

    private void UpdateScore()
    {
        _scoreText.text = _score.ToString();
    }
}
