using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] SpawnManager _spawnManager;

    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] GameObject _gameClearText;
    private Canvas _canvas;
    private AudioSource _audioSource;
    private Player _player;
    
    [SerializeField] private int _bossHealth = 10;
    
    private float _delay = 9f;



    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
        _spawnManager = GameObject.FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>();
        _canvas = GameObject.FindObjectOfType<Canvas>(); 

        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayDelayed(_delay);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().Damage();
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _bossHealth--;
            if (_bossHealth <= 0)
            {
                BossDeath();
            }
        }
    }

    private void BossDeath()
    {
        _player.AddScore(100);
        GameObject gameClearText = Instantiate(_gameClearText, _canvas.transform.position, Quaternion.identity);
        gameClearText.transform.SetParent(_canvas.transform);

        //VFX Explosion
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}