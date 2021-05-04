using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //enemy config
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyContainer;

    //power up config
    [SerializeField] private float _minPosX, _maxPosX, _startPosY; 
    [SerializeField] private float _minSpawnIntervalPowerup = 3f;
    [SerializeField] private float _maxSpawnIntervalPowerup = 7f;
    [SerializeField] private GameObject[] PowerUpPrefabs;
    [SerializeField] private int _minPowerupID = 0;
    [SerializeField] private int _maxPowerupIDplusOne = 3;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        //null check enemy prefab
        if (enemyPrefab == null)
        {
            Debug.LogError("Spawnmanager.enemyprefab is NULL");
        }
        if (enemyContainer == null)
        {
            Debug.LogError("Spawnmanager.enemycontainer is NULL");
        }
        //if (PowerUpPrefabs[] == null)
        //{
        //    Debug.LogError("Spawnmanager.powerup is NULL");
        //}
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {        
                    
    }

    IEnumerator SpawnEnemyRoutine()
    {
        //while player is alive
        while (_stopSpawning == false)
        {
            //spawn enemy
            GameObject newEnemy = Instantiate(enemyPrefab);
            //set parent to container
            newEnemy.transform.parent = enemyContainer.transform;
            //wait for seconds
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (_stopSpawning == false)
        {
            //random spawn time
            float spawnIntervalPowerup = Random.Range(_minSpawnIntervalPowerup, _maxSpawnIntervalPowerup);
            //wait first
            yield return new WaitForSeconds(spawnIntervalPowerup);
            //random spawn pos
            Vector3 spawnPos = new Vector3(Random.Range(_minPosX, _maxPosX), _startPosY, 0);
            //spawn random powerup
            Instantiate(PowerUpPrefabs[Random.Range(_minPowerupID, _maxPowerupIDplusOne)], spawnPos, Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
