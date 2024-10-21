using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AntDragObject : MonoBehaviour
{
    private GameObject _draggedObject;
    private bool _isDragging;
    private AntMovement _antMovement;

    // Start is called before the first frame update
    void Start()
    {
        _isDragging = false;

        _antMovement = GetComponent<AntMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDragging)
        {
            _draggedObject.transform.position = transform.position;
            if(Vector3.Distance(_antMovement.startPos, transform.position) < 0.1f)
            {
                // TODO - Add function for ant cake retrieve
                Destroy(_draggedObject.gameObject);
                Destroy(this.gameObject);
            }
        }
    }

    public void Drag(GameObject draggedObject)
    {
        _draggedObject = draggedObject;
        _isDragging = true;
    }

    public void StopDrag()
    {
        _isDragging = false;
    }
}
