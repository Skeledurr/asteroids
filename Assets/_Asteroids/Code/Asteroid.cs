using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Values")] 
    [SerializeField] private Vector2 _speedRange;
    [SerializeField] private Vector2 _rotationRange;
    
    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;

    private CameraBounds _cameraBounds;
    private Vector2 _wrappedPos;

    public void Initialise(CameraBounds cameraBounds)
    {
        _cameraBounds = cameraBounds;
        SetRandomVelocity();
    }

    private void LateUpdate()
    {
        BoundsCheck();
    }

    void BoundsCheck()
    {
        if (_cameraBounds.WrapPosition(_rigidbody.position, out _wrappedPos))
        {
            _rigidbody.position = _wrappedPos;
        }
    }

    private void SetRandomVelocity()
    {
        // Direction.
        _rigidbody.linearVelocity = Random.insideUnitCircle.normalized * Random.Range(_speedRange.x, _speedRange.y);
        
        // Spin
        _rigidbody.angularVelocity = new Vector3(Random.Range(_rotationRange.x, _rotationRange.y),
                                        Random.Range(_rotationRange.x, _rotationRange.y),
                                        Random.Range(_rotationRange.x, _rotationRange.y));
    }
}
