using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _explosionAudioClip;

    private SpawnManager _spawnManager;

    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("EnemyDamage.player is NULL");
        }
        if (_animator == null)
        {
            Debug.LogError("EnemyDamage.animator is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("EnemyDamage.audiosource is NULL");
        }
        else
        {
            _audioSource.clip = _explosionAudioClip;
        }
        _spawnManager = FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("EnemyDamage.spawnmanager is NULL");
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
        //play death anim
        _animator.SetTrigger("OnEnemyDeath");
        //destroy collider?
        Destroy(GetComponent<PolygonCollider2D>());

        //Destroy childeren (thrusters)
        //Debug.Log(transform.childCount);
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        //play explosion SFX
        _audioSource.Play();

        _spawnManager.DecreaseEnemiesLeft();

        //wait for animmation to end
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
