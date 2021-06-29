using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveCircular : MonoBehaviour
{
    private float _angle = 90f;
    [SerializeField] private float _speed = 0.5f; //(2 * Mathf.PI) / 5f; //2*PI in degress is 360, so you get 5 seconds to complete a circle
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _centerPosX, _centerPosY;
    private float _x, _y;

    // Start is called before the first frame update
    void Start()
    {
        _centerPosX = Random.Range(-8f, 8f);
        _centerPosY = Random.Range(8f, 9f);
        _radius = Random.Range(5f, 12f);
        _speed = Random.Range(0.5f, 1f);
        if (Random.value > 0.5f)
        {
            _speed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _angle += _speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
        _x = Mathf.Cos(_angle) * _radius + _centerPosX;
        _y = Mathf.Sin(_angle) * _radius + _centerPosY;
        transform.position = new Vector3(_x, _y, 0);
    }
}
