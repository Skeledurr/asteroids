using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Values")] 
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _speedRngRange;
    [SerializeField] private float _rotationRngRange;
    
    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;

    private CameraBounds _cameraBounds;
    private Vector2 _wrappedPos;
    private float _baseSpeedMultiplier = 1f;

    public void Initialise(CameraBounds cameraBounds, float baseSpeedMultiplier)
    {
        _cameraBounds = cameraBounds;
        _baseSpeedMultiplier = baseSpeedMultiplier;
        SetRandomVelocity();
    }

    private void LateUpdate()
    {
        BoundsCheck();
    }

    private void BoundsCheck()
    {
        if (_cameraBounds.WrapPosition(_rigidbody.position, out _wrappedPos))
        {
            _rigidbody.position = _wrappedPos;
        }
    }

    private void SetRandomVelocity()
    {
        float speed = (_baseSpeed * _baseSpeedMultiplier) + Random.Range(-_speedRngRange, _speedRngRange);
        
        // Direction.
        _rigidbody.linearVelocity = Random.insideUnitCircle.normalized * speed;
        
        // Spin
        _rigidbody.angularVelocity = new Vector3(Random.Range(-_rotationRngRange, _rotationRngRange),
                                                Random.Range(-_rotationRngRange, _rotationRngRange),
                                                Random.Range(-_rotationRngRange, _rotationRngRange));
    }
}
