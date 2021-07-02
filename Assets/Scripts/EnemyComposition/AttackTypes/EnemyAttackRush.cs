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

    [SerializeField] private float _minPosY = -8f;
    [SerializeField] private float _maxPosX = 9.5f;
    [SerializeField] private float _minPosX = -9.5f;

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
            //change direction to movement direction
            _direction = (transform.position - _targetPos).normalized;       
                float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle + 270f, Vector3.forward);
        }
        else
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0,0,0);
            if (transform.position.y <= _minPosY)
            {
                transform.position = new Vector3(Random.Range(_minPosX, _maxPosX), transform.position.y * -1, 0);
            }
        }
    }
}
