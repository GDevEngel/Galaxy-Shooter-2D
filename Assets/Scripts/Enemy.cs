using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _minPosY = -8f;
    [SerializeField] private float _maxPosX = 9.5f;
    [SerializeField] private float _minPosX = -9.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //if below min position down spawn at top again
        if (transform.position.y <= _minPosY)
        {
            transform.position = new Vector3(Random.Range(_minPosX, _maxPosX), transform.position.y * -1, 0);
        }
    }
}
