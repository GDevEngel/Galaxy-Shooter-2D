using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //move laser down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //destroy laser if off bottom screen
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //damage player
            Player player = other.GetComponent<Player>();
            player.Damage();
            //destroy self
            Destroy(this.gameObject);
        }
    }
}
