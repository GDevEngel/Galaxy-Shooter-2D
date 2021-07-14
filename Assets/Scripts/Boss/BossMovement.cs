using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;

    void Start()
    {
        if (Random.value > 0.5f)
        {
            _speed *= -1;
        }
    }

    void Update()
    {
        //Debug.Log(Mathf.Sin(Time.time));
        transform.position = new Vector3(_speed * Mathf.Sin(Time.time), transform.position.y, 0);
    }    
}
