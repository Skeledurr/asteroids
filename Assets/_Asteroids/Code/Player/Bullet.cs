using System;
using UnityEngine;

/// <summary>
/// Bullet is the Player's projectile.
/// This class handles setting up and handling collision events
/// for the bullet.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(WrapPosition))]
public class Bullet : PoolMember
{
    [Header("Values")]
    [SerializeField] private float _speed = 15f;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }
    
    private void OnEnable()
    {
        _rigidbody.linearVelocity = this.transform.up * _speed;
    }

    private void OnDisable()
    {
        _rigidbody.linearVelocity = Vector3.zero;
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
