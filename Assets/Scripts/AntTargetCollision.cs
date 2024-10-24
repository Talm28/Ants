using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntTargetCollision : MonoBehaviour
{
    private AntMovement _antMovement;
    private AntDragObject _andDragObject;
    private AntState _antState;
    private bool _tookCake;

    private GameObject _draggedCake;

    // Start is called before the first frame update
    void Start()
    {
        _antMovement = GetComponent<AntMovement>();
        _andDragObject = GetComponent<AntDragObject>();
        _antState = GetComponent<AntState>();

        _tookCake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_tookCake && transform.position == _antMovement.startPos)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Cake")
        {
            CakeController cakeController = other.gameObject.GetComponent<CakeController>();
            if(cakeController != null && !cakeController.isTaken && _antState.State == MovementState.CakeTargeted) // Check if the cake is avaiable and took it if so
            {
                cakeController.onCakeTook.RemoveListener(_antMovement.ActiveWondering); // Remove the current ant from cake event
                cakeController.TakeCake();
                _antMovement.ReturnToStart(_antMovement.startPos);
                _andDragObject.Drag(other.gameObject);
                _draggedCake = other.gameObject;
            }
        }
        else if(other.gameObject.tag == "Bullet") // Bullet collision
        {
            Destroy(other.gameObject);
            if(_draggedCake != null) // If ant has cake release it
            {
                CakeController cakeController = _draggedCake.gameObject.GetComponent<CakeController>();
                if(cakeController != null) cakeController.ReleaseCake();
            }
            Destroy(this.gameObject);
        }
    }
}
