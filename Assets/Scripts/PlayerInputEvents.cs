using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputEvents : MonoBehaviour
{
    public static event System.Action OnPlayerInput;

    // Statická metoda pro zjištìní, zda bylo stisknuto nìjaké tlaèítko
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

