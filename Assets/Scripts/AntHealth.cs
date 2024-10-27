using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AntHealth : MonoBehaviour
{
    public int Health {get; private set;}
    [SerializeField] private int _startHealth;
    [SerializeField] private float _killDelayTime;

    private ScoreController _scoreController;
    public UnityEvent DemageTaken;
    public UnityEvent OnAntKill;

    private AntDragObject _antDragObject;

    void Awake() {
        Health = _startHealth;
    }

    public void Start() {
        _antDragObject = GetComponent<AntDragObject>();
        _scoreController = GameObject.FindGameObjectWithTag("Score manager").GetComponent<ScoreController>();

        OnAntKill.AddListener(KillAnt);
        
    }

    public void TakeHealth()
    {
        Health--;

        if( Health <= 0 )
            OnAntKill.Invoke();
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

        _scoreController.AddScore(this.gameObject);
        Destroy(this.gameObject);// Need to add delay for particels...
    }

}
