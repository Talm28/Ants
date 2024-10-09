using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidBody;

    private bool _isReady = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

    }

    // Rotate the bullet and make it fly
    public void SetTarget(Vector2 target, Vector3 origin)
    {
        Rotate(target, origin);

        _isReady = true;
    }

    private void Rotate(Vector3 target, Vector3 origin)
    {
        // Calculate target direction
        Vector2 direction = target - origin;
        // Calculate angle
        float angle = Vector2.SignedAngle(direction, origin);
        // Rotate cannon
        transform.rotation = Quaternion.Euler(0, 0, 180 - angle);
    }

    void Update()
    {
        if(_isReady)
            transform.position += transform.up * _speed * Time.deltaTime;
    }

    // Destroy the bullet if he goes off the screen
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
