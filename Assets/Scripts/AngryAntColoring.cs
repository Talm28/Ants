using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryAntColoring : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    private SpriteRenderer _spriteRenderer;
    private AntHealth _antHealth;
    private ParticleSystem _particleSystem;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _antHealth = GetComponent<AntHealth>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        
        _antHealth.DemageTaken.AddListener(UpdateColor);
    }

    private void Start() {
        UpdateColor();
    }
    
    public void UpdateColor()
    {
        if(_antHealth.Health <= 1)
            _particleSystem.Stop();
        _spriteRenderer.color = _colors[_antHealth.Health - 1];
    }
}
