using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public abstract class Controller : MonoBehaviour
{
    protected Rigidbody _rigidBody;
    protected Animator _animator;

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();
    }
}
