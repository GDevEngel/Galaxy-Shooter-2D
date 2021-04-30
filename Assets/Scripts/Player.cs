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

    [SerializeField] private float _fireRate = 0.25f;
    private float _nextFire = 0f;
    private Vector3 _laserOffset = new Vector3(0f, 0.75f, 0f);
    public GameObject LaserPrefab; 

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

        //When fire is pressed AND current game time is greater then previous laser fire time + fire rate(cooldown)
        if (Input.GetButton("Fire1") && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate; 
            //instantite laser + Y offset
            Instantiate(LaserPrefab, transform.position +_laserOffset, Quaternion.identity);
        }

    }

    private void PlayerMovement()
    {
        //move player based on user input
        transform.Translate(Input.GetAxis("Horizontal") * _speed * Time.deltaTime, Input.GetAxis("Vertical") * _speed * Time.deltaTime, 0);

        //player position restriction on Y axis using Mathf.Clamp
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _minPosY, _maxPosY), 0);

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
