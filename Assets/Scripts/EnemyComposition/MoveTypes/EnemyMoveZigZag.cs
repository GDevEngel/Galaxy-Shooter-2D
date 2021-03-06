using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveZigZag : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _switchTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.value > 0.5f)
        {
            _speed *= -1;
        }
        StartCoroutine(ZigZagSwitch());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);        
    }

    IEnumerator ZigZagSwitch()
    {
        while (true)
        {
            _speed *= -1;            
            yield return new WaitForSeconds(_switchTime);
        }
    }
}
