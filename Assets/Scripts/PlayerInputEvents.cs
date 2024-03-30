using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputEvents : MonoBehaviour
{
    public static event System.Action OnPlayerInput;

    // Statick� metoda pro zji�t�n�, zda bylo stisknuto n�jak� tla��tko
    public static bool IsAnyButtonPressed()
    {
        return Input.GetKeyDown(KeyCode.C) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.X);
    }

    public static bool PisPressed()
    {
        return Input.GetKeyDown(KeyCode.P);
    }

    public static bool EscPressed()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    void Update()
    {
        if (IsAnyButtonPressed())
        {
            OnPlayerInput?.Invoke();
        }
    }
}

