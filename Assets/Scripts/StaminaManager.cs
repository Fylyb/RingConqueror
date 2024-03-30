using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class StaminaManager : MonoBehaviour
{
    public float maxStamina = 100f;
    public float staminaRecoveryRate = 10f;

    private float _currentStamina;

    public Slider staminaSlider;

    private void Start()
    {
        _currentStamina = maxStamina;
        
    }

    private void Update()
    {
        // Obnovování staminy
        if (_currentStamina < maxStamina)
        {
            _currentStamina += staminaRecoveryRate * Time.deltaTime;
            _currentStamina = Mathf.Clamp(_currentStamina, 0f, maxStamina);

            staminaSlider.value = _currentStamina / maxStamina;
        }
    }

    public bool UseStamina(float amount)
    {
        if (_currentStamina >= amount)
        {
            _currentStamina -= amount;
            return true;
        }
        return false;
    }

    public float GetCurrentStamina()
    {
        //print($"Players stamina: {_currentStamina}");
        return _currentStamina;
    }

    public void ResetStamina()
    {
        _currentStamina = maxStamina;
        staminaSlider.value = 1f; // Aktualizovat UI Slider
    }
}
