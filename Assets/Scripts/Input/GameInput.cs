using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnSecondInteractAction;

    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();


        playerInputActions.Player.SecondInteract.performed += SecondInteractPerformed;
        playerInputActions.Player.Interact.performed += InteractPerformed;
    }

    private void SecondInteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSecondInteractAction?.Invoke(this, new BoolEventArgs(obj.ReadValue<float>() == 1));
    }
    private void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}

public class BoolEventArgs : EventArgs
{
    public bool Condition;
    public BoolEventArgs(bool condition) {
        Condition = condition;
    }
}
