using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : ChefController
{

    public static List<PlayerController> players = new();
    private GameInput _gameInput;
    private CharacterController _characterController;

    protected override void Start()
    {
        base.Start();
        
        _characterController = GetComponent<CharacterController>();

        // Connect Input
        _gameInput = GameInput.Instance;
        _gameInput.OnInteractAction += ChefInteract;
        _gameInput.OnSecondInteractAction += ChefSecondInteract;
        players.Add(this);
    }


    protected override void UpdateMovement()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 direction = new(inputVector.x, 0, inputVector.y);

        transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * 7.5f);
        _characterController.SimpleMove(direction * 5f);
        bool isMoving = _characterController.velocity.magnitude > 0.1f;
        _animator.SetBool("IsMoving", isMoving);
    }

    protected void OnDestroy()
    {
        players.Remove(this);
    }
}
