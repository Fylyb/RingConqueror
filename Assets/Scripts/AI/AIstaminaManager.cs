using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AIstaminaManager : MonoBehaviour
{
    public float maxStamina = 100f;
    public float staminaRecoveryRate = 10f;

    private float _currentStamina;

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
        //print($"AI stamina: {_currentStamina}");
        return _currentStamina;
    }

    public void RestoreStamina(float amount)
    {
        _currentStamina = Mathf.Clamp(amount, 0f, maxStamina);
    }
}
