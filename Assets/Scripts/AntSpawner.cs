using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] targets = new GameObject[2];
    [SerializeField] private GameObject[] ants = new GameObject[1];
    [SerializeField] private float _timeToSpawn;

    private float _timer;
    private Camera _cameraMain;
    // Start is called before the first frame update
    void Start()
    {
        _cameraMain = Camera.main;
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
        GameObject target = targets[Random.Range(0,2)];
        GameObject ant = ants[Random.Range(0,1)];
        Vector3 position = _cameraMain.ViewportToWorldPoint(new Vector3(Random.Range(0f,1f), 1, 0));
        position.z = 0;
        GameObject antGameobject = Instantiate(ant, position, Quaternion.identity);
        antGameobject.GetComponent<AntMovement>().SetTarget(target.transform.position);
    }
}
