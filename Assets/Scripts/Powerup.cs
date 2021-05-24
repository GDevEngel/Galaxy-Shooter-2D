using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _minPosY = -8f;
    [SerializeField] private float _speed = 3f;

    [SerializeField] private AudioClip _powerupSFX;

    //Powerup IDs
    //0 = triple shot, 1 = speed, 2 = shield
    [SerializeField] private int powerupID;

    // Start is called before the first frame update
    void Start()
    {
        //mass homing missle powerup self destruct to reduce spawn rate
        if (powerupID == 5 && Random.value > 0.5f)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
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
}
