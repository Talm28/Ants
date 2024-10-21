using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AntSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] targetCakes = new GameObject[2];
    [SerializeField] private GameObject[] ants = new GameObject[1];
    [SerializeField] private float _timeToSpawn;

    [SerializeField] private List<GameObject> _cakePool;
    public UnityEvent onCakePoolRefill;
    private float _timer;
    private Camera _cameraMain;
    // Start is called before the first frame update
    void Start()
    {
        _cameraMain = Camera.main;

        _cakePool = new List<GameObject>();

        foreach(GameObject cake in targetCakes)
            _cakePool.Add(cake);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > _timeToSpawn)
        {
            _timer = 0;
            SpawnAnts();
        }
    }

    private void SpawnAnts()
    {
        // Spawn ant in random position
        GameObject ant = ants[Random.Range(0,1)];
        Vector3 position = _cameraMain.ViewportToWorldPoint(new Vector3(Random.Range(0f,1f), 1, 0));
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

}
