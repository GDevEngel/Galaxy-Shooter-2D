using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBackstab : MonoBehaviour
{
    [SerializeField] private float _fireRate = 0.4f;
    private float _nextFire = 0f;

    [SerializeField] private GameObject _enemyShot;
    private Vector3 _offset = new Vector3(0, -0.9f, 0);
    private bool _isAlive = true;

    private GameObject _player;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        if (_player == null)
        {
            Debug.LogError("EnemyAttackBackstab.player is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlive) // double check to prevent bug that executes while loop while enemy is already dead
        {
            if (GetComponent<PolygonCollider2D>() != null)
            {
                if ((_player.transform.position.y - this.transform.position.y) > 1f && Time.time > _nextFire)
                {
                    _nextFire = Time.time + _fireRate;
                    Instantiate(_enemyShot, transform.position + _offset, Quaternion.identity);
                }
            }
        }
    }
}
