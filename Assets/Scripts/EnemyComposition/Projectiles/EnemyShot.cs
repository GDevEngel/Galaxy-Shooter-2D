using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _targetPlayer;
    [SerializeField] private float _speed = 2f;
    private Vector3 _direction; 

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        //null check into get current target location
        if (_player != null)
        {
            _targetPlayer = _player.transform.position;
        }
        else
        {
            Destroy(this.gameObject);
            Debug.LogError("EnemyShot.player is NULL");
        }
        //calculate direction to move (normalized scales values of vector to be max 1)
        _direction = (_targetPlayer - transform.position).normalized * _speed;

        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * Time.deltaTime);

        //self destruct if there is no target anymore
        if (_player == null)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
    }
}
