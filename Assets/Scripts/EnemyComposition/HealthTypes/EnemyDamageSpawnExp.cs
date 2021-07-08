﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageSpawnExp : MonoBehaviour
{
    private Player _player;
    private bool _isAlive;

    [SerializeField] GameObject _explosionPrefab;

    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();    
        if (_player == null)
        {
            Debug.LogError("EnemyDamage.player is NULL");
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.tag == "Player")
        {
            _player.Damage();
            _player.AddScore(5);
            EnemyDeath();
        }
        else if (other.tag == "Laser")
        {
            //destroy laser
            Destroy(other.gameObject);
            _player.AddScore(10);
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        //to stop fire routine
        _isAlive = false;
        //Destroy childeren (thrusters)
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        //Spawn Explosion
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        
        Destroy(GetComponent<Collider2D>());    // coz of the 0.5f delay of the explosion for VFX reasons
        Destroy(this.gameObject, 0.5f);
    }
}

