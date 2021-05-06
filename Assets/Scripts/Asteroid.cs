using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private int _speedRotation;
    [SerializeField] GameObject explosionPrefab;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _speedRotation = Random.Range(-70, 70);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        //null checks
        if (explosionPrefab == null)
        {
            Debug.LogError("Asteroid.explosionPrefab is NULL");
        }
        if (_spawnManager == null)
        {
            Debug.LogError("Asteroid.spawnmanager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rotate on Z
        transform.Rotate(Vector3.forward * _speedRotation * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            //instantiate explosion
            Instantiate(explosionPrefab);
            //destroy laser
            Destroy(other.gameObject);
            //start spawning 
            _spawnManager.StartSpawning();
            //destroy self
            Destroy(this.gameObject, 1f);
        }
    }

}
