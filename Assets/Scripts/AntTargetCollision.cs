using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntTargetCollision : MonoBehaviour
{
    private AntMovement _antMovement;
    private AntDragObject _andDragObject;
    private AntState _antState;
    private AntHealth _antHealth;

    private bool _tookCake;

    // Start is called before the first frame update
    void Start()
    {
        _antMovement = GetComponent<AntMovement>();
        _andDragObject = GetComponent<AntDragObject>();
        _antState = GetComponent<AntState>();
        _antHealth = GetComponent<AntHealth>();

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
                _andDragObject.Drag(other.gameObject);
                cakeController.TakeCake();
                _antMovement.ReturnToStart(_antMovement.startPos);
            }
        }
        else if(other.gameObject.tag == "Bullet") // Bullet collision
        {
            Destroy(other.gameObject);
            _antHealth.TakeHealth();
        }
    }
}
