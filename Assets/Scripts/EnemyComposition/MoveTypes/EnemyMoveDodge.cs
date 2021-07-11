using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveDodge : MonoBehaviour
{
    [SerializeField] private float _dodgeRate = 1.5f;
    private float _dodgeRange = 1.5f;
    private float _nextDodge = 0f;
    private Vector2 _boxSize = new Vector2(2.5f,0.01f);
    private float _maxDistance = 20f;
    private float _angle = 0f;

    private Vector3 _offset = new Vector3(0, -2f, 0);

    private Collider2D _collider;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _dodgeSFX;

    private void Start()
    {
        _collider = GetComponent<PolygonCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        if (_collider == null)
        {
            Debug.LogError("EnemyMoveDodge.collider is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("EnemyMoveDodge.audiosource is NULL");
        }
    }
    
    void Update()
    {
        if (_collider)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position + _offset, _boxSize, _angle, Vector2.down, _maxDistance);
            // If raycast hits tag Laser
            // Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Laser" && Time.time > _nextDodge)
            {
                _nextDodge = Time.time + _dodgeRate;

                //randomize dodge direction
                if (Random.value > 0.5)
                {
                    _dodgeRange *= -1f;
                }
                transform.Translate(Vector3.left * _dodgeRange);

                // dodge SFX
                _audioSource.PlayOneShot(_dodgeSFX, 1f);
            }
        }
    }
}
