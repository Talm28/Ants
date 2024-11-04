using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AntSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _targetCakes = new GameObject[2];
    [SerializeField] private GameObject[] _ants = new GameObject[2];
    [SerializeField] private int _speedyAntProb;
    [SerializeField] private int _angryAntProb;
    [SerializeField] private int _startTimeTospawn;
    [SerializeField] private float _timeToSpawn;

    [SerializeField] private List<GameObject> _cakePool;
    public UnityEvent onCakePoolRefill;
    private bool _isActive;
    private float _timer;
    private Camera _cameraMain;
    private System.Random _rnd;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _cameraMain = Camera.main;

        _cakePool = new List<GameObject>();

        _gameManager = GameObject.FindGameObjectWithTag("Game manager").GetComponent<GameManager>();
        _gameManager.onGameOver.AddListener(StopSpawner);

        _rnd = new System.Random();

        _timeToSpawn = _startTimeTospawn;

        _isActive = true;

        foreach(GameObject cake in _targetCakes)
            _cakePool.Add(cake);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isActive)
        {
            _timer += Time.deltaTime;

            if(_timer > _timeToSpawn)
            {
                _timer = 0;
                SpawnAnts();
                if(_timeToSpawn > 0.9f)
                    _timeToSpawn -= 0.01f;
            }
        }
    }

    private void SpawnAnts()
    {
        // Spawn ant in random position
        GameObject ant = GetAnt();
        Vector3 position = _cameraMain.ViewportToWorldPoint(new Vector3(Random.Range(0.1f,0.9f), 1, 0));
        position.z = 0;
        GameObject antGameobject = Instantiate(ant, position, Quaternion.identity);
        // Update ant target
        if(_cakePool.Count == 0) antGameobject.GetComponent<AntMovement>().ActiveWondering();
        else 
        {
            GameObject target = _cakePool[Random.Range(0,_cakePool.Count)];
            antGameobject.GetComponent<AntMovement>().SetTarget(target.transform.position, target);
        }
    }

    private GameObject GetAnt()
    {
        if(_rnd.Next(0, _angryAntProb) == 0)
            return _ants[2];
        else if(_rnd.Next(0, _speedyAntProb) == 0)
            return _ants[1];
        return _ants[0];
    }

    public void AddToCakePool(GameObject cake)
    {
        _cakePool.Add(cake);
        // Give all wondering ants new target
        onCakePoolRefill?.Invoke();
        onCakePoolRefill.RemoveAllListeners();
    }

    public void RemoveFromCakePool(GameObject cake)
    {
        _cakePool.Remove(cake);
    }

    public GameObject GetCakeTarget()
    {
        if(_cakePool.Count > 0)
            return _cakePool[Random.Range(0,_cakePool.Count)];
        return null;
    }

    private void StopSpawner()
    {
        _isActive = false;
    }

}
