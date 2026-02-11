using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(WrapPosition))]
public class Asteroid : MonoBehaviour
{
    [Header("Values")] 
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _speedRngRange;
    [SerializeField] private float _rotationRngRange;
    
    private Rigidbody _rigidbody;
    private AsteroidManager _manager;
    private float _baseSpeedMultiplier = 1f;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    public void Initialise(AsteroidManager manager, float baseSpeedMultiplier)
    {
        _manager = manager;
        _baseSpeedMultiplier = baseSpeedMultiplier;
        SetRandomVelocity();
    }

    public void OnBulletHit(Bullet bullet)
    {
        // TODO determine damage.
        // TODO start VFX + animations.
        // TODO Create Children
        _manager.OnAsteroidDestroyed(this);
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
