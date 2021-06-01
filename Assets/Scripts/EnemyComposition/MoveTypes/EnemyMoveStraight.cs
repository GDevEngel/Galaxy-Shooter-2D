using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveStraight : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

    }
}
