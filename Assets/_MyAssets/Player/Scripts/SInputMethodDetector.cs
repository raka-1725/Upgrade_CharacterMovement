using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class SInputMethodDetector : MonoBehaviour
{
    public enum InputMethod
    {
        KeyboardMouse,
        Controller
    }

    public static InputMethod CurrentInputMethod { get; private set; }

    public static event Action<InputMethod> OnInputMethodChanged;

    private void OnEnable()
    {
        InputSystem.onEvent += OnInputEvent;
    }

    private void OnDisable()
    {
        InputSystem.onEvent -= OnInputEvent;
    }

    private void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
    {
        // Ignore events that don't contain actual state changes.
        if (!eventPtr.IsA<StateEvent>() &&
            !eventPtr.IsA<DeltaStateEvent>())
            return;

        InputMethod? method = null;

        if (device is Gamepad)
        {
            method = InputMethod.Controller;
        }
        else if (device is Keyboard || device is Mouse)
        {
            method = InputMethod.KeyboardMouse;
        }

        if (method == null || method.Value == CurrentInputMethod)
            return;

        CurrentInputMethod = method.Value;
        OnInputMethodChanged?.Invoke(CurrentInputMethod);

        Debug.Log($"Input method changed: {CurrentInputMethod}");
    }
}
