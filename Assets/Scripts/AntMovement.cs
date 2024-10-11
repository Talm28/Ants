using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{
    [SerializeField] private Vector2 _speed;
    [SerializeField] float _noiseScaleMax;
    [SerializeField] private Vector2 _size;
    [SerializeField] private Vector2 _stopTime;

    private float _currSpeed;
    private Vector3 _target;
    private float _noiseYValue;
    private float _timer;
    private float _stopAndTimer;
    private bool _canMove;
    private bool _isStopped;
    private float _noiseScale;
    private float _maxDisntance;



    void Awake()
    {
        _canMove = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        _currSpeed = Random.Range(_speed.x, _speed.y);

        _noiseYValue = Random.Range(0f, 1000f);

        _timer = 0;
        _stopAndTimer = 0;

        float size = Random.Range(_size.x, _size.y);
        transform.localScale = new Vector3(size, size, size);

        _isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if(_canMove)
        {
            Rotate(_target);
            if(! _isStopped)
                Movement();
                
            StopGenerator();
        }
    }

    public void SetTarget(Vector3 target)
    {
        target.z = 0;

        _target = target;
        _canMove = true;

        _maxDisntance = Vector3.Distance(transform.position, target);
    }

    private void Movement()
    {
        transform.position += transform.up * _currSpeed * Time.deltaTime;
    }

    private void Rotate(Vector3 target)
    {
        // Calculate current distance
        float distance = Vector3.Distance(transform.position, target);

        // Calculate direction
        Vector2 direction = target - transform.position;

        // Calculate noise scale
        _noiseScale = Mathf.Lerp(1, _noiseScaleMax, distance / _maxDisntance);

        // Calculate angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Calculate angle noise
        float noiseRaw = Mathf.PerlinNoise(_timer, _noiseYValue);
        float noiseScaled = noiseRaw * 2 - 1;
        noiseScaled *= _noiseScale;
        angle += noiseScaled;

        // Rotate the ant
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    private void StopGenerator()
    {
        if(_isStopped) return;

        _stopAndTimer += Time.deltaTime;

        if(_stopAndTimer > 1)
        {
            _stopAndTimer = 0;
            if(Random.Range(0,5) == 0)
                StopAnt();
        }

        
    }

    private void StopAnt()
    {
        _isStopped = true;

        float waitTime = Random.Range(_stopTime.x, _stopTime.y);

        StartCoroutine(StopAndCoroutine(waitTime));
    }
    private IEnumerator StopAndCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _isStopped = false;
    }
}
