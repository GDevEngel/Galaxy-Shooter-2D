﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _minPosY = -8f;
    [SerializeField] private float _speed = 3f;

    //Powerup IDs
    //0 = triple shot, 1 = speed, 2 = shield
    [SerializeField] private int powerupID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // when passed bottom of screen
        if (transform.position.y < _minPosY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if collision tag is Player
        if (other.tag == "Player")
        {
            //get player script //activate power up triple shot
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {                
                switch (powerupID)
                {
                    case 0:
                        player.PowerUpPickUp("TripleShot");
                        break;
                    case 1:
                        player.PowerUpPickUp("SpeedUp");
                        //Debug.Log("SPEED POWER UP");
                        break;
                    case 2:
                        player.PowerUpPickUp("Shield");
                        //Debug.Log("SHIELD POWER UP");
                        break;
                    default:
                        Debug.Log("Default value in switch stement of powerupID");
                            break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}