using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AntDragObject : MonoBehaviour
{

    public GameObject draggedObject;
    private AntMovement _antMovement;
    private AntState _antState;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _antMovement = GetComponent<AntMovement>();
        _antState = GetComponent<AntState>();
        _gameManager = GameObject.FindGameObjectWithTag("Game manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_antState.State == MovementState.Returning)
        {
            draggedObject.transform.position = transform.position;
            if(Vector3.Distance(_antMovement.startPos, transform.position) < 0.1f)
                RetrieveCake();
        }
    }

    private void RetrieveCake()
    {
        _gameManager.CakeRetrive();
        
        Destroy(draggedObject.gameObject);
        Destroy(this.gameObject);
    }

    public void Drag(GameObject draggedObject)
    {
        this.draggedObject = draggedObject;
    }
}
