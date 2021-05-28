using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private int _speedRotation;
    private float _speed;
    private float _scale;
    private Vector3 _scaleChange;

    [SerializeField] GameObject explosionPrefab;

    private float _minPosY = -10f;
    
    // Start is called before the first frame update
    void Start()
    {
        _speedRotation = Random.Range(-70, 70);
        _speed = Random.Range(1f, 7f);
        _scale = Random.Range(-0.9f, 0.6f);
        _scaleChange = new Vector3(_scale, _scale, _scale);

        transform.localScale += _scaleChange;

        //null checks
        if (explosionPrefab == null)
        {
            Debug.LogError("Asteroid.explosionPrefab is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //rotate on Z
        transform.Rotate(Vector3.forward * _speedRotation * Time.deltaTime);

        transform.position += Vector3.down * _speed * Time.deltaTime;

        if (_minPosY > transform.position.y)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            //destroy laser
            Destroy(other.gameObject);

            AsteroidDestroy();

        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.PowerUpPickUp("Asteroid");
            player.Damage();

            AsteroidDestroy();
        }
    }

    private void AsteroidDestroy()
    {
        //instantiate explosion
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale += _scaleChange;

        //destroy self
        Destroy(GetComponent<Collider2D>());
        Destroy(this.gameObject, 0.5f);
    }

}
