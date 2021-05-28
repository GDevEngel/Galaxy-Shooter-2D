using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _minPosY = -8f;
    [SerializeField] private float _speed = 3f;

    [SerializeField] private AudioClip _powerupSFX;

    private bool _isMagnetActive = false;
    private GameObject _player;
    private float _magnetSpeed = 4f;
    private Vector3 _direction;
    private Rigidbody2D rb;

    //Powerup IDs
    //0 = triple shot, 1 = speed, 2 = shield
    [SerializeField] private int powerupID;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        if (_player == null)
        {
            Debug.Log("Powerup.player is NULL");
        }

        rb = GetComponent<Rigidbody2D>();

        //mass homing missle powerup self destruct to reduce spawn rate
        if (powerupID == 5 && Random.value > 0.5f)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMagnetActive)
        {
            _direction = (_player.transform.position - this.transform.position).normalized * _magnetSpeed;                       
            rb.velocity = _direction;
        }
        else 
        {
            //move down
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        // when passed bottom of screen
        if (transform.position.y < _minPosY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if collision tag is Player
        if (other.tag == "Player")
        {
            //SFX
            //AudioSource.PlayClipAtPoint(_powerupSFX, transform.position);
            AudioSource.PlayClipAtPoint(_powerupSFX, Camera.main.transform.position);

            //get player script //activate power up triple shot
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {                
                switch (powerupID)
                {
                    case 0:
                        player.PowerUpPickUp("TripleShot");
                        break;
                    case 1:
                        player.PowerUpPickUp("SpeedUp");
                        //Debug.Log("SPEED POWER UP");
                        break;
                    case 2:
                        player.PowerUpPickUp("Shield");
                        //Debug.Log("SHIELD POWER UP");
                        break;
                    case 3:
                        player.PowerUpPickUp("Repair");
                        break;
                    case 4:
                        player.PowerUpPickUp("Ammo");
                        break;
                    case 5:
                        player.PowerUpPickUp("MassHomingMissile");
                        break;
                    default:
                        Debug.Log("Default value in switch stement of powerupID");
                            break;
                }
            }
            Destroy(this.gameObject);
        }
    }

    public void EnableMagnet()
    {
        _isMagnetActive = true;
    }
}
