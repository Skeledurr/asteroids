using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WrapPosition : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector2 _wrappedPos;
    
    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (GameController.CameraBounds.WrapPosition(_rigidbody.position, out _wrappedPos))
        {
            _rigidbody.position = _wrappedPos;
        }
    }
}