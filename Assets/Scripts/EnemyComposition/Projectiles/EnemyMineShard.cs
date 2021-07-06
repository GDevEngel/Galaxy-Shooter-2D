using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMineShard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.Damage();

            Destroy(this.gameObject);
        }

    }
}
