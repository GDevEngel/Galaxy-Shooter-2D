using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveLeftRight : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;


    private float _minPosX = -12f;
    private float _maxPosX = 12f;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.value > 0.5f)
        {
            _speed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);

        //wrap around if moved off screen left and right
        if (transform.position.x > _maxPosX)
        {
            transform.position = new Vector3(_minPosX, transform.position.y, 0);
        }
        if (transform.position.x < _minPosX)
        {
            transform.position = new Vector3(_maxPosX, transform.position.y, 0);
        }

    }
}
