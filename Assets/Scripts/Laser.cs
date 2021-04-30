using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //destroy laser if off upper screen
        if (transform.position.y > 10)
        {
            Destroy(gameObject);
        }
    }
}
