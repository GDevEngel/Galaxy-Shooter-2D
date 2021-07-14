using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackGiantLaser : MonoBehaviour
{
    [SerializeField] private float _minInterval = 10f;
    [SerializeField] private float _maxInterval = 30f;

    [SerializeField] private GameObject _giantLaser;

    void Start()
    {
        StartCoroutine(FireRoutine());
    }

    IEnumerator FireRoutine()
    {
        yield return new WaitForSeconds(Random.Range(_minInterval, _maxInterval));
        GameObject giantLaser = Instantiate(_giantLaser, transform.position, Quaternion.identity);
        giantLaser.transform.SetParent(this.transform);
    }
}
