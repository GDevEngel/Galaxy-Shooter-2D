using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRush : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _minDistance = 4f;
    private float _rushSpeed = 4f;
    private Vector3 _targetPos;
    private Vector2 _direction; 

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
        {
            Debug.LogError("EnemyAttackRush.player is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) <= _minDistance)
        {
            _targetPos = _player.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, _rushSpeed * Time.deltaTime);

            _direction = (transform.position - _targetPos).normalized;       
                float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle + 270f, Vector3.forward);
        }
        //Debug.Log(Vector3.Distance(transform.position, _player.transform.position));
        else
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        /*
        _direction = (transform.position - _targetPos).normalized;
        if (_direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 270f, Vector3.forward);
        }
        */
    }
}
