using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackTurret : MonoBehaviour
{
    [SerializeField] private float _fireRate = 3f;
    private float _nextFire = 0f;
    private float _speed = 3f;
    [SerializeField] private float _rotationSpeed = 90f;
    private float _rotation;
    [SerializeField] private float _offset = 180f;

    [SerializeField] private GameObject _enemyLaser;
    
    void Update()
    {
        _rotation = _rotationSpeed * Mathf.Sin(Time.time);
        _rotation += _offset;
        transform.rotation = Quaternion.Euler(0, 0, _rotation);

        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            GameObject bossLaser = Instantiate(_enemyLaser, transform.position, transform.rotation);
                        
            Vector3 movement = transform.rotation * new Vector3(0, _speed, 0);
            bossLaser.GetComponent<Rigidbody2D>().velocity = movement;
        }
    }
}
