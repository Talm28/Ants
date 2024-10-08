using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonMovement : MonoBehaviour
{
    private Transform _cannonTransform;

    void Start()
    {
        _cannonTransform = GetComponent<Transform>();

    }

    public void Rotate(Vector3 target)
    {
        // Calculate target direction
        Vector2 direction = target - _cannonTransform.position;
        // Calculate angle
        float angle = Vector2.SignedAngle(direction, _cannonTransform.position);
        // Rotate cannon
        _cannonTransform.rotation = Quaternion.Euler(0, 0, 180 - angle);
    }
}
