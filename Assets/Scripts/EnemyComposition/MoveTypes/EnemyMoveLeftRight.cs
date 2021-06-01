using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveLeftRight : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        if (Random.value > 0.5f)
        {
            _speed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
