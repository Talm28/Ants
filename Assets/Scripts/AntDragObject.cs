using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AntDragObject : MonoBehaviour
{
    private GameObject _draggedObject;
    private AntMovement _antMovement;
    private AntState _antState;

    // Start is called before the first frame update
    void Start()
    {
        _antMovement = GetComponent<AntMovement>();
        _antState = GetComponent<AntState>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_antState.State == MovementState.Returning)
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
    }
}
