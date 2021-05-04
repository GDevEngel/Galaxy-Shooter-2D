﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //movement config
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _maxPosX = 11.4f;
    [SerializeField] private float _minPosX = -11.4f;
    [SerializeField] private float _maxPosY = 4f;
    [SerializeField] private float _minPosY = -4.2f;

    //laser var
    [SerializeField] private float _fireRate = 0.25f;
    private float _nextFire = 0f;
    private Vector3 _laserOffset = new Vector3(0f, 1f, 0f);
    [SerializeField] private GameObject LaserPrefab;
    [SerializeField] private GameObject LaserTriplePrefab;
    [SerializeField] private bool _isTripleLaserActive;

    [SerializeField] private int _health = 3;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        //set starting position
        transform.position = new Vector3(0, -3, 0);

        _isTripleLaserActive = false;

        //find gameobject then get component
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        //null check
        if (_spawnManager == null)
        {
            Debug.LogError("Player.spawn manager is NULL");
        }
        if (LaserPrefab == null)
        {
            Debug.LogError("Player.Laserprefab is NULL");
        }
        if (LaserTriplePrefab == null)
        {
            Debug.LogError("Player.LaserTriplePrefab is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        //When fire is pressed AND current game time is greater then previous laser fire time + fire rate(cooldown)
        if (Input.GetButton("Fire1") && Time.time > _nextFire)
        {
            FireLaser();
        }

    }

    public void Damage()
    {
        _health--;
        if (_health <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    private void FireLaser()
    {
        _nextFire = Time.time + _fireRate;

        //if is triple laser active
        if (_isTripleLaserActive == true)
        {
            //instantiate 3 lasers
            Instantiate(LaserTriplePrefab, transform.position + _laserOffset, Quaternion.identity);
        }
        //else fire 1 laser
        else {
            //instantite laser + Y offset
            Instantiate(LaserPrefab, transform.position + _laserOffset, Quaternion.identity);
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

    public void PowerUpPickUp(string PowerUpType)
    {
        if (PowerUpType == "TripleShot")
        {
            _isTripleLaserActive = true;
            //start coroutine
            StartCoroutine(TripleShotCooldown());
        }
    }

    IEnumerator TripleShotCooldown()
    {
        yield return new WaitForSeconds(5f);
        _isTripleLaserActive = false;
    }
}
