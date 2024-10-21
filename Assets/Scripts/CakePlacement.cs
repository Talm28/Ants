using UnityEngine;

public class CakePlacement : MonoBehaviour
{
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Vector2 _cakeOffset;

    private Camera _cameraMain;

    private void Awake() {

        _cameraMain = Camera.main;
        Vector3 wroldPos = _cameraMain.ViewportToWorldPoint(_targetPosition);
        wroldPos.x += _cakeOffset.x;
        wroldPos.y += _cakeOffset.y;
        wroldPos.z = 0;
        transform.position = wroldPos;
    }
}
