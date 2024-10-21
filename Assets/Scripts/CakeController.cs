using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CakeController : MonoBehaviour
{
    [SerializeField] private AntSpawner _antSpawner;
    [SerializeField] private float _draggedAlpha;

    public UnityEvent onCakeTook;
    public bool isTaken = false;

    private SpriteRenderer _spriteRenderer;

    public void Start() {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void TakeCake()
    {
        isTaken = true;
        _antSpawner.RemoveFromCakePool(this.gameObject);
        ChangeAlpha(_draggedAlpha);
        // Change all ant that directed to this cake to wondering mode
        onCakeTook?.Invoke();
        onCakeTook.RemoveAllListeners();
    }

    public void ReleaseCake()
    {
        isTaken = false;
        _antSpawner.AddToCakePool(this.gameObject);
        ChangeAlpha(1f);
    }

    private void ChangeAlpha(float alpha)
    {
        Color color = _spriteRenderer.color;
        color.a = alpha;
        _spriteRenderer.color = color;
    }

}
