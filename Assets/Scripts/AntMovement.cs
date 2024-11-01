using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AntMovement : MonoBehaviour
{
    [SerializeField] private Vector2 _speedRange;
    [SerializeField] private Vector2 _noiseScaleRange;
    [SerializeField] private Vector2 _sizeRange;
    [SerializeField] private Vector2 _stopTimeRange;

    public Vector3 startPos;

    private AntSpawner _antSpawner;
    private AntState _antState;
    private float _currSpeed;
    private Vector3 _target;
    private GameObject _targetGameObject;
    private float _noiseYValue;
    private float _timer;
    private float _stopAndTimer;
    private bool _isStopped;
    private float _noiseScaleMax;
    private float _noiseScale;
    private float _maxDisntance;

    void Awake()
    {
        _antSpawner = GameObject.FindGameObjectWithTag("AntSpawner").GetComponent<AntSpawner>();
        _antState = GetComponent<AntState>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currSpeed = Random.Range(_speedRange.x, _speedRange.y);

        _noiseYValue = Random.Range(0f, 1000f);

        _noiseScaleMax = Random.Range(_noiseScaleRange.x, _noiseScaleRange.y);

        _timer = 0;
        _stopAndTimer = 0;

        float size = Random.Range(_sizeRange.x, _sizeRange.y);
        transform.localScale = new Vector3(size, size, size);

        _isStopped = false;

        startPos = transform.position;
    }

    void OnDisable()
    {
        // Remove ant from events
        _antSpawner.onCakePoolRefill.RemoveListener(DeactiveWondering);

        if(_targetGameObject != null)
        {
             CakeController cakeController = _targetGameObject.GetComponent<CakeController>();
            if(cakeController != null) cakeController.onCakeTook.AddListener(ActiveWondering);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if(_antState.State != MovementState.Start)
        {
            Rotate(_target);

            if(_antState.State == MovementState.Wondering)
                AntWondering();

            if(! _isStopped)
                Movement();
                
            StopGenerator();
        }
    }

    public void SetTarget(Vector3 target, GameObject? targetGameObject)
    {
        target.z = 0;
        _target = target;
        _targetGameObject = targetGameObject;

        _maxDisntance = Vector3.Distance(transform.position, target);

        if(targetGameObject != null)
        {
            CakeController cakeController = targetGameObject!.GetComponent<CakeController>();
            if(cakeController != null) // If the target is Cake
            {
                cakeController.onCakeTook.AddListener(ActiveWondering);
                _antState.SetState(MovementState.CakeTargeted);
            }
        }
    }

    public void ReturnToStart(Vector3 target)
    {
        target.z = 0;
        _target = target;

        _maxDisntance = Vector3.Distance(transform.position, target);

        _antState.SetState(MovementState.Returning);
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

    protected virtual void StopGenerator()
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

        float waitTime = Random.Range(_stopTimeRange.x, _stopTimeRange.y);

        StartCoroutine(StopAndCoroutine(waitTime));
    }
    private IEnumerator StopAndCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _isStopped = false;
    }

    private void AntWondering()
    {
        if(Vector3.Distance(transform.position, _target) <= 0.5f)
            SetRandomTarget();
    }

    private void SetRandomTarget()
    {
        Vector3 newPos = new Vector3(Random.Range(0f,1f), Random.Range(0.2f,1f), 0);
        newPos = Camera.main.ViewportToWorldPoint(newPos);
        SetTarget(newPos, null);
    }

    public void ActiveWondering()
    {
        _targetGameObject = null;

        SetRandomTarget();

        _antSpawner.onCakePoolRefill.AddListener(DeactiveWondering);

        _antState.SetState(MovementState.Wondering);
    }

    public void DeactiveWondering()
    {
        GameObject target = _antSpawner.GetCakeTarget();
        SetTarget(target.transform.position, target);
    }

}
