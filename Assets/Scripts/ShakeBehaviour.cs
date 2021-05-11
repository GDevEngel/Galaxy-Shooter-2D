using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehaviour : MonoBehaviour
{
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeMagnitude = 0.08f;
    [SerializeField] private float _dampingSpeed = 3f;
    private Vector3 _initialPos;

    // Start is called before the first frame update
    void Start()
    {
        _initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shakeDuration > 0)
        {
            transform.position = _initialPos + Random.insideUnitSphere * _shakeMagnitude;

            _shakeDuration -= Time.deltaTime * _dampingSpeed;
        }
        else
        {
            transform.position = _initialPos;
        }
    }

    public void CameraShake()
    {
        _shakeDuration = 2f;
    }
}
