using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{   
    [SerializeField] private GameObject _bullet;
    [SerializeField] private CannonBarController _cannonBarController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnBullet(Vector2 target)
    {
        if(_cannonBarController.CanShoot())
        {
            _cannonBarController.Shoot();
            
            GameObject bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletMovement>().SetTarget(target, transform.position);
        }
        
    }
}
