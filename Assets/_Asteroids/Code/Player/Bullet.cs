using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(WrapPosition))]
public class Bullet : PoolMember
{
    [Header("Values")]
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _duration = 1f;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }
    
    private void OnEnable()
    {
        _rigidbody.linearVelocity = this.transform.up * _speed;
    }

    private void Update()
    {
        DurationUpdate();
    }

    private void DurationUpdate()
    {
        _duration -= Time.deltaTime;

        if (_duration <= 0)
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        // TODO Object Pool
        Destroy(this.gameObject);
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
