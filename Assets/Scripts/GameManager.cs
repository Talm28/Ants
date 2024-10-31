using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUI;

    public UnityEvent onGameOver;
    private int _cakeNumber;

    // Start is called before the first frame update
    void Start()
    {
        _cakeNumber = GameObject.FindGameObjectsWithTag("Cake").Length;   
        onGameOver.AddListener(GameOver); 
    }

    public void CakeRetrive()
    {
        _cakeNumber--;

        if( _cakeNumber == 0 )
        {
            onGameOver?.Invoke();
            // Stop ant spawner
        }
    }

    private void GameOver()
    {
        _gameOverUI.SetActive(true);
    }
}
