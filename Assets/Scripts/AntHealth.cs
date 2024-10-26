using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AntHealth : MonoBehaviour
{
    public int Health {get; private set;}
    [SerializeField] private int _startHealth;
    [SerializeField] private float _killDelayTime;

    public UnityEvent DemageTaken;

    private AntDragObject _antDragObject;

    void Awake() {
        Health = _startHealth;
    }
    public void Start() {
        _antDragObject = GetComponent<AntDragObject>();
        
    }

    public void TakeHealth()
    {
        Health--;

        if( Health <= 0 )
            KillAnt();
        else
            DemageTaken?.Invoke();
    }

    private void KillAnt()
    {
        GameObject draggedObject = _antDragObject.DraggedObject;
        if(draggedObject != null) // If ant has cake release it
        {
            CakeController cakeController = draggedObject.gameObject.GetComponent<CakeController>();
            if(cakeController != null) cakeController.ReleaseCake();
        }
        Destroy(this.gameObject);// Need to add delay for particels...
    }

}
