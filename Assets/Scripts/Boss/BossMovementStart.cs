using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovementStart : MonoBehaviour
{
    private float _speed = 2f;
    private Vector3 _target = new Vector3(0, 1.5f, 0);

    private BossMovement _bossMove;
    private BossHealth _bossHealth;
    [SerializeField] private GameObject TurretL;
    [SerializeField] private GameObject TurretR;

    private void Start()
    {
        _bossMove = this.GetComponent<BossMovement>();
        _bossMove.enabled = false;
        _bossHealth = this.GetComponent<BossHealth>();
        _bossHealth.enabled = true;

        TurretL.SetActive(false);
        TurretR.SetActive(false);
        //disable TurretL and TurrentR
    }


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _target) < 0.001f)
        {
            _bossMove.enabled = true;
            TurretL.SetActive(true);
            TurretR.SetActive(true);

            this.GetComponent<BossMovementStart>().enabled = false;
        }
    }
}
