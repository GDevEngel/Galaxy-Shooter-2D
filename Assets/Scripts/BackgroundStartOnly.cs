using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStartOnly : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 1.5f;
    [SerializeField] private float _minPosY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _scrollSpeed * Time.deltaTime);
        if (transform.position.y < _minPosY)
        {
            Destroy(this.gameObject);
        }

    }
}
