using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{   
    [SerializeField] private GameObject _bullet;
    [SerializeField] private CannonBarController _cannonBarController;

    private bool _isActive;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _isActive = true;

        _gameManager = GameObject.FindGameObjectWithTag("Game manager").GetComponent<GameManager>();
        _gameManager.onGameOver.AddListener(StopBulletSpawning);
    }

    public void SpawnBullet(Vector2 target)
    {
        if(_isActive && _cannonBarController.CanShoot())
        {
            _cannonBarController.Shoot();
            
            GameObject bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletMovement>().SetTarget(target, transform.position);
        }
        
    }

    public void StopBulletSpawning()
    {
        _isActive = false;
    }
}
