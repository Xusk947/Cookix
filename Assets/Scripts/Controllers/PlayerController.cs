using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : ChefController
{
    public static List<PlayerController> players = new List<PlayerController>();

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
        Vector3 direction = new Vector3(inputVector.x, 0, inputVector.y);

        Vector3 position = transform.position + direction * Time.deltaTime * 5f;
        transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * 7.5f);
        _characterController.SimpleMove(direction * 5f);
        _animator.SetBool("IsMoving", _characterController.velocity.magnitude > 0.1f);
    }
}
