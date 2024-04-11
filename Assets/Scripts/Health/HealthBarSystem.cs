using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSystem : MonoBehaviour
{

    [SerializeField] private Image _HealthBarSlider;
    [SerializeField] private GameObject _Camera;
    

    public void UpdateHealthBar(float CurrentHealth, float MaxHealth)
    {
        _HealthBarSlider.fillAmount = CurrentHealth / MaxHealth;
    }
    private void Update()
    {
       if(_Camera)
       transform.rotation = _Camera.transform.rotation;
       
       
    }
}
