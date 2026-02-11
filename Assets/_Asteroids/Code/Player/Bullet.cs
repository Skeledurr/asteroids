using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(WrapPosition))]
public class Bullet : PoolMember
{
    [Header("Values")]
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _duration = 1f;
    
    private Rigidbody _rigidbody;
    private float _curDuration;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }
    
    private void OnEnable()
    {
        _rigidbody.linearVelocity = this.transform.up * _speed;
        _curDuration = _duration;
    }

    private void Update()
    {
        DurationUpdate();
    }

    private void DurationUpdate()
    {
        _curDuration -= Time.deltaTime;

        if (_curDuration <= 0)
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        ReturnToPool();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Asteroid asteroid = other.GetComponent<Asteroid>();
        if (asteroid)
        {
            asteroid.OnBulletHit(this);
            DestroyBullet();
        }
    }
}
