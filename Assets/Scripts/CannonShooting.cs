using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooting : MonoBehaviour
{
    [SerializeField] private CannonBarController _cannonBarController;
    [SerializeField] private GameObject _shotParticles;
    [SerializeField] private GameObject _hitShotParticles;
    [SerializeField] private GameObject _shotLine;
    [SerializeField] private Transform _shotSpot;

    private GameManager _gameManager;
    private bool _isActive;

    private void Start() {
        _isActive = true;  

        _gameManager = GameObject.FindGameObjectWithTag("Game manager").GetComponent<GameManager>();
        _gameManager.onGameOver.AddListener(StopCannonShooting);  
    }

    public void Shot()
    {
        if(_isActive && _cannonBarController.CanShoot())
        {
            _cannonBarController.Shoot();
            
            // Spawn particle
            GameObject fireParticle = Instantiate(_shotParticles, _shotSpot.position, transform.rotation);
            Destroy(fireParticle, 0.5f);
            
            // Raycast staff
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up), 15);

            if(hit.collider != null)
            {
                AntHealth targetAntHealth = hit.collider.gameObject.GetComponent<AntHealth>();
                if(targetAntHealth != null)
                {
                    GameObject shotLine = Instantiate(_shotLine, Vector3.zero, Quaternion.identity);
                    shotLine.GetComponent<ShotLineController>().SetPoints(transform, targetAntHealth.transform);
                    Destroy(shotLine, 0.08f);

                    GameObject hitParticle = Instantiate(_hitShotParticles, targetAntHealth.transform.position, Quaternion.identity);
                    Destroy(hitParticle, 0.5f);

                    targetAntHealth.TakeHealth(); 
                }
                      
            }
            
        }
    }

    public void StopCannonShooting()
    {
        _isActive = false;
    }
}
