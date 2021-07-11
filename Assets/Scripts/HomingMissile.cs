using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] private GameObject[] _targets;
    [SerializeField] private GameObject _target;

    private Rigidbody2D rb;

    [SerializeField] private float _speed = 4f;

    private Vector3 _direction;
           
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 10f);
    }  
    
    public void AssignTarget(GameObject PowerUpTarget)
    {
        _target = PowerUpTarget;
    }

    // Update is called once per frame
    void Update()
    {
        _direction = (_target.transform.position - transform.position).normalized * _speed;

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rb.velocity = new Vector2(_direction.x, _direction.y);
        //transform.Translate(_direction * Time.deltaTime * _directionModifier); this results is a tendecy for rockets to circle left??

        //check for boxcolider coz that gets destroyed before the gameobject in our case for vfx
        if (_target.gameObject.GetComponent<PolygonCollider2D>() == null || _target.gameObject == null)
        {
            FindClosestEnemy();
        }
    }

    private void FindClosestEnemy()
    {
        float distanceToCloestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in allEnemies)
        {
            //Magnitude is the 'length/power' of the vector. square magnitude is to reduce sys load because when comparing distances it uses root and square prevents that or something
            float distanceToEnemy = (enemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToCloestEnemy)
            {
                distanceToCloestEnemy = distanceToEnemy;
                closestEnemy = enemy;
            }
        }
        _target = closestEnemy;
    }
}
