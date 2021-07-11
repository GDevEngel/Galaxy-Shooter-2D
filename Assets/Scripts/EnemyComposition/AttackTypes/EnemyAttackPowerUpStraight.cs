using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackPowerUpStraight : MonoBehaviour
{
    [SerializeField] private float _fireRate = 1f;
    private float _nextFire = 0f;

    [SerializeField] private GameObject _enemyLaser;
    private Vector3 _offset = new Vector3(0, -0.9f, 0);

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PolygonCollider2D>() != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + _offset, Vector2.down);
            //Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "PowerUp" && Time.time > _nextFire)
            {
                _nextFire = Time.time + _fireRate;
                Instantiate(_enemyLaser, transform.position + _offset, Quaternion.identity);
            }
        }
        
    }
}
