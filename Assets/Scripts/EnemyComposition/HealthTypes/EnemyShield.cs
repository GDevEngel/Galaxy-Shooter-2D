using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    private Player _player;

    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] private GameObject _shield;
    private bool _isShieldActive = true;

    //handle
    private SpawnManager _spawnManager;


    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
        _shield.SetActive(true);

        if (_player == null)
        {
            Debug.LogError("EnemyShield.player is NULL");
        }
        _spawnManager = FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("EnemyShield.spawnmanager is NULL");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other);
        if (_isShieldActive == true && (other.tag == "Player" || other.tag == "Laser"))
        {
            _shield.SetActive(false);
            _isShieldActive = false;

            if (other.tag == "Player")
            {
                //dmg player
                _player.Damage();
            }
            else if (other.tag == "Laser")
            {
                //destroy laser
                Destroy(other.gameObject);
            }
        }
        else { 
            //if other is player
            if (other.tag == "Player")
            {
                //dmg player
                _player.Damage();
                //add 10 to score
                _player.AddScore(10);                
                EnemyDeath();
            }
            //if other is laser
            else if (other.tag == "Laser")
            {
                //destroy laser
                Destroy(other.gameObject);
                //add score
                _player.AddScore(20);
                EnemyDeath();
            }
        }
    }

    private void EnemyDeath()
    {
        //to stop fire routine
        //Destroy childeren (thrusters)
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        //Spawn Explosion
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        _spawnManager.DecreaseEnemiesLeft();

        Destroy(GetComponent<Collider2D>());    // coz of the 0.5f delay of the explosion for VFX reasons
        Destroy(this.gameObject, 0.5f);
    }
}
