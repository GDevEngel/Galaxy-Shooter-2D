using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotatingGiantLaser : MonoBehaviour
{
    //giant laser config
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _TurnOnGiantLaserDelay = 1.5f;
    [SerializeField] private float _damageInterval = 0.5f;
    [SerializeField] private float _nextDamage = 1.5f;
    [SerializeField] private float _minDuration = 10f;
    [SerializeField] private float _maxDuration = 20f;

    //handle
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _giantLaserSFX;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        StartCoroutine(TurnOnGiantLaser());
        _nextDamage = _TurnOnGiantLaserDelay + Time.time;

        //random rotate direction
        if (Random.value > 0.5f)
        {
            _speed *= -1;
        }

        Destroy(gameObject, Random.Range(_minDuration, _maxDuration));
    }

    IEnumerator TurnOnGiantLaser()
    {
        yield return new WaitForSeconds(_TurnOnGiantLaserDelay);
        _spriteRenderer.enabled = true;
        _audioSource.clip = _giantLaserSFX;
        _audioSource.loop = true;
        _audioSource.Play();
    }


    // Update is called once per frame
    void Update()
    {
        if (_spriteRenderer.enabled == true)
        {
            transform.Rotate(Vector3.forward * (_speed * Time.deltaTime));
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && _nextDamage < Time.time)
        {
            _nextDamage = Time.time + _damageInterval;
            
            Player player = other.GetComponent<Player>();
            player.Damage();
        }
    }
}
