using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _minPosY = -8f;
    [SerializeField] private float _maxPosX = 9.5f;
    [SerializeField] private float _minPosX = -9.5f;

    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _explosionAudioClip;
    [SerializeField] private GameObject _enemyShot;
    [SerializeField] private float _fireInterval = 10f;
    private Vector3 _offset = new Vector3(0, -0.9f, 0);
    private bool _isAlive;

    // Start is called before the first frame update
    void Start()
    {
        _isAlive = true;


        //spawn at bottom out of vision to trigger respawn at top
        transform.position = new Vector3(transform.position.x, _minPosY * 2, 0);

        _player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
        _animator = GameObject.FindObjectOfType<Enemy>().GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Enemy.player is NULL");
        }
        if (_animator == null)
        {
            Debug.LogError("Enemy.animator is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Enemy,audiosource is NULL");
        }
        else
        {
            _audioSource.clip = _explosionAudioClip;
        }

        StartCoroutine(FireRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //if below min position down spawn at top again at random posX
        if (transform.position.y <= _minPosY)
        {
            transform.position = new Vector3(Random.Range(_minPosX, _maxPosX), transform.position.y * -1, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other);

        //if other is player
        if (other.tag == "Player")
        {        
            //dmg player
            _player.Damage();
            //add 10 to score
            _player.AddScore(5);
            //destroy us
            StartCoroutine(EnemyDeath());
        }
        //if other is laser
        else if (other.tag == "Laser")
        {
            //destroy laser
            Destroy(other.gameObject);
            //add score
            _player.AddScore(10);
            //destroy us
            StartCoroutine(EnemyDeath());
        }
    }

    IEnumerator EnemyDeath()
    {
        //to stop fire routine
        _isAlive = false;
        //play death anim
        _animator.SetTrigger("OnEnemyDeath");
        //destroy collider?
        Destroy(GetComponent<BoxCollider2D>());
        //play explosion SFX
        _audioSource.Play();
        //wait for animmation to end
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

    IEnumerator FireRoutine()
    {
        while (_isAlive)
        {
            yield return new WaitForSeconds(_fireInterval);
            Instantiate(_enemyShot, transform.position + _offset, Quaternion.identity);
        }
    }

}