using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackStraight : MonoBehaviour
{
    [SerializeField] private float _fireRate = 3f;
    private float _nextFire = 0f;

    [SerializeField] private GameObject _enemyLaser;
    private Vector3 _offset = new Vector3(0, -0.9f, 0);
    private bool _isAlive = true;

    // Update is called once per frame
    void Update()
    {
        if (_isAlive) // double check to prevent bug that executes while loop while enemy is already dead
        {
            if (GetComponent<PolygonCollider2D>() != null)
            {
                if (Time.time > _nextFire)
                {
                    _nextFire = Time.time + _fireRate;
                    Instantiate(_enemyLaser, transform.position + _offset, Quaternion.identity);
                }
            }
        }
    }
}
