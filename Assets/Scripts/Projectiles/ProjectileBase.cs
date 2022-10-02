using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] private DamageType type = DamageType.Normal;
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 2;
    [SerializeField] private ParticleSystem destroyParticle;

    private GameObject _owner;
    private Vector3 _direction;

    private bool _isInitialized = false;
    private bool _shouldMove = true;

    public void Initialize(Vector3 direction, GameObject instigator)
    {
        Vector2 velocity = new Vector2(direction.x, direction.y).normalized * speed;
        _direction = new Vector3(velocity.x, velocity.y, 0);
        _owner = instigator;
        _isInitialized = true;
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        if (!_isInitialized || !_shouldMove) return;
        
        transform.position += _direction * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _owner) return;

        if (col.gameObject.TryGetComponent<IDamageInterface>(out IDamageInterface damageInterface))
        {
            damageInterface.Damage(type, damage);
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        if (destroyParticle)
        {
            _shouldMove = false;
            Destroy(gameObject, .5f);
            destroyParticle.Play();
        }
        else
            Destroy(gameObject);
    }
}
