using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMine : MonoBehaviour
{
    [SerializeField] private float _DownSpeed = 1f;

    [SerializeField] GameObject _mineShard;
    [SerializeField] private float _shardSpeed = 3f;

    private GameObject _player;
    [SerializeField] private float _minDistance = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
        {
            Debug.LogError("EnemyMine.player is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move mine down
        transform.Translate(Vector3.down * _DownSpeed * Time.deltaTime);

        //destroy mine if off bottom screen
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }


        if (Vector3.Distance(transform.position, _player.transform.position) <= _minDistance)
        {
            //Debug.Log("EnemyMine: player got close");
            
            for (int i = 0; i < 8; i++)
            {
                //tbh dont know why it's 1/7. mb coz 1st angle will be zero coz of i. just found it by tweaking the number...
                //changed it to a random range coz it seemed a tiny bit off
                float angle = i * Mathf.PI * 2f / (1/Random.Range(7f,8f));
                //Debug.Log("angle: "+angle);
                GameObject mineShard = Instantiate(_mineShard, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));

                //quaternion x vector ORDER MATTERS
                Vector3 movement = Quaternion.AngleAxis(angle, Vector3.forward) * new Vector3(0, _shardSpeed, 0);

                mineShard.GetComponent<Rigidbody2D>().velocity = movement;
                
        }
            //Destroy mine
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            //destroy self
            Destroy(this.gameObject);
        }
    }
}
