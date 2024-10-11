using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{
    [SerializeField] private Vector2 _speed;
    [SerializeField] float _noiseScaleMax;
    [SerializeField] private Vector2 _size;

    private float _currSpeed;
    private Vector3 _target;
    private float _noiseYValue;
    private float _timer;
    private bool _canMove;
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

        float size = Random.Range(_size.x, _size.y);
        transform.localScale = new Vector3(size, size, size);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if(_canMove)
        {
            Rotate(_target);
            Movement();
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
}
