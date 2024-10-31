using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CannonBarController : MonoBehaviour
{
    [SerializeField] private int _shotAmount;
    [SerializeField] private float _fillTime;

    private Slider _slider;
    private TextMeshProUGUI _text;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _text = GetComponentInChildren<TextMeshProUGUI>();

        _slider.maxValue = _shotAmount;
        _slider.value = _slider.maxValue;
        UpdateText();
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_slider.value < _slider.maxValue)
        {
            _timer += Time.deltaTime;

            if(_timer >= _fillTime)
            {
                _slider.value += 1;
                UpdateText();
                _timer = 0;
            }
        }
        else
            _timer = 0;
        
    }

    public void Shoot()
    {
        _slider.value -= 1;
        UpdateText();
    }

    public bool CanShoot() => _slider.value >= 1;

    private void UpdateText()
    {
        _text.text = _slider.value.ToString();
    }
}
