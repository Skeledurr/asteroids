using UnityEngine;

/// <summary>
/// Wrap Position uses <see cref="GameBounds"/> to determine if
/// this MonoBehaviour needs to wrap their position to the other
/// side of the screen when exceeding the bounds.
/// </summary>
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
        if (GameController.GameBounds.WrapPosition(_rigidbody.position, out _wrappedPos))
        {
            _rigidbody.position = _wrappedPos;
        }
    }
}