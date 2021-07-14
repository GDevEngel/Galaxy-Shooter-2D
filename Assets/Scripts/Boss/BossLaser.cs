using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    private float _selfDestroyTimer = 8f;

    void Start()
    {
        Destroy(gameObject, _selfDestroyTimer);        
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
