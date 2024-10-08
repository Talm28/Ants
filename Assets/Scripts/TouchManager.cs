using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private Camera _mainCamera;

    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _mainCamera = Camera.main;

        _touchPositionAction = _playerInput.actions["TouchPosition"];
        _touchPressAction = _playerInput.actions["TouchPress"];
    }

    // Subscribe to the touch event
    private void OnEnable()
    {
        _touchPressAction.started += TouchPressed;
    }

    // Unsubscribe to the touch event
    private void OnDisable()
    {
        _touchPressAction.canceled -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector2 position = _touchPositionAction.ReadValue<Vector2>();

        Vector3 wordPosition = _mainCamera.ScreenToWorldPoint(position);
        wordPosition.z = 0;

        Debug.Log(wordPosition);
    }
}