using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(WrapPosition))]
public class Asteroid : PoolMember
{
    public int PointValue => _configData.PointValue;
    
    private AsteroidConfigData _configData;
    private Rigidbody _rigidbody;
    private AsteroidManager _manager;
    private float _baseSpeedMultiplier = 1f;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    public void Initialise(AsteroidManager manager, AsteroidConfigData config, float baseSpeedMultiplier)
    {
        _manager = manager;
        _configData = config;
        _baseSpeedMultiplier = baseSpeedMultiplier;
        SetRandomVelocity();
    }

    public void OnBulletHit(Bullet bullet)
    {
        GameController.ObjectPool.Spawn(_configData.ExplosionType, this.transform.position, Quaternion.identity);
        CreateChildren();
        _manager.OnAsteroidDestroyed(this);
    }

    private void CreateChildren()
    {
        for (int i = 0; i < _configData.SplitCount; i++)
        {
            _manager.SpawnAsteroid(_configData.SplitAsteroidType, _rigidbody.position);
        }
    }

    private void SetRandomVelocity()
    {
        // Direction.
        float speed = (_configData.BaseSpeed * _baseSpeedMultiplier) + Random.Range(0f, _configData.SpeedRngRange);
        Vector2 dir = Random.insideUnitCircle.normalized;
        _rigidbody.linearVelocity = dir * speed;
        
        // Spin
        _rigidbody.angularVelocity = new Vector3(Random.Range(-_configData.RotationRngRange, _configData.RotationRngRange),
                                                Random.Range(-_configData.RotationRngRange, _configData.RotationRngRange),
                                                Random.Range(-_configData.RotationRngRange, _configData.RotationRngRange));
    }
}
