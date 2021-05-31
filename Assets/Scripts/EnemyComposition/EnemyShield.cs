using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _explosionAudioClip;
    private bool _isAlive = true;

    [SerializeField] private GameObject _shield;
    private bool _isShieldActive = true;

    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _shield.SetActive(true);

        if (_player == null)
        {
            Debug.LogError("EnemyShield.player is NULL");
        }
        if (_animator == null)
        {
            Debug.LogError("EnemyShield.animator is NULL");
        }
        if (_shield == null)
        {
            Debug.LogError("EnemyShield.shield is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("EnemyShield.audiosource is NULL");
        }
        else
        {
            _audioSource.clip = _explosionAudioClip;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other);
        if (_isShieldActive == true)
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
                //destroy us
                StartCoroutine(EnemyDeath());
            }
            //if other is laser
            else if (other.tag == "Laser")
            {
                //destroy laser
                Destroy(other.gameObject);
                //add score
                _player.AddScore(20);
                //destroy us
                StartCoroutine(EnemyDeath());
            }
        }
    }

    IEnumerator EnemyDeath()
    {
        //to stop fire routine
        _isAlive = false;
        //play death anim
        _animator.SetTrigger("OnEnemyDeath");
        //destroy collider?
        Destroy(GetComponent<PolygonCollider2D>());
        //play explosion SFX
        _audioSource.Play();
        //wait for animmation to end
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
