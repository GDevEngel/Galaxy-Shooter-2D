using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _maxPosX = 11.4f;
    [SerializeField] private float _minPosX = -11.4f;
    [SerializeField] private float _maxPosY = 4f;
    [SerializeField] private float _minPosY = -4.2f;

    // Start is called before the first frame update
    void Start()
    {
        //set starting position
        transform.position = new Vector3(0, -3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        //move player based on user input
        transform.Translate(Input.GetAxis("Horizontal") * _speed * Time.deltaTime, Input.GetAxis("Vertical") * _speed * Time.deltaTime, 0);

        //player position restriction on Y axis
        if (transform.position.y >= _maxPosY)
        {
            transform.position = new Vector3(transform.position.x, _maxPosY, 0);
        }
        else if (transform.position.y <= _minPosY)
        {
            transform.position = new Vector3(transform.position.x, _minPosY, 0);
        }
        //player position wrap around on X axis
        if (transform.position.x >= _maxPosX)
        {
            transform.position = new Vector3(_minPosX, transform.position.y, 0);
        }
        else if (transform.position.x <= _minPosX)
        {
            transform.position = new Vector3(_maxPosX, transform.position.y, 0);
        }
    }
}
