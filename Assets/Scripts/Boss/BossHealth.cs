using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] GameObject _explosionPrefab;

    private int _bossHealth = 10;

    private AudioSource _audioSource;
    private float _delay = 7f;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayDelayed(_delay);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //VFX Explosion
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
