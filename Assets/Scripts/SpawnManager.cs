using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //enemy config
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private GameObject enemyContainer;

    [SerializeField] private int[] _enemyWeightTable;
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private int _enemyWeightTotal = 0;
    [SerializeField] private int randomEnemyNumber;



    [SerializeField] private float _minPosX, _maxPosX, _startPosY;

    //power up config
    [SerializeField] private float _minSpawnIntervalPowerup = 3f;
    [SerializeField] private float _maxSpawnIntervalPowerup = 7f;

    [SerializeField] private int[] _powerUpWeightTable;
    [SerializeField] private GameObject[] powerUpsPrefabs;
    [SerializeField] private int _total = 0;
    [SerializeField] private int randomNumber;


    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        //null check enemy prefab
        /*if (enemyPrefab == null)
        {
            Debug.LogError("Spawnmanager.enemyprefab is NULL");
        }*/
        if (enemyContainer == null)
        {
            Debug.LogError("Spawnmanager.enemycontainer is NULL");
        }
        //if (PowerUpPrefabs[] == null)
        //{
        //    Debug.LogError("Spawnmanager.powerup is NULL");
        //}

        StartSpawning();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
        //TO DO enable background scroll scripts
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        foreach (var weight in _enemyWeightTable)
        {
            _enemyWeightTotal += weight;
        }

        //while player is alive
        while (_stopSpawning == false)
        {
            //spawn enemy
            randomEnemyNumber = Random.Range(0, _enemyWeightTotal);

            for (int i = 0; i < _enemyWeightTable.Length; i++)
            {
                if (randomEnemyNumber <= _enemyWeightTable[i])
                {
                    Vector3 spawnPos = new Vector3(Random.Range(_minPosX, _maxPosX), _startPosY, 0);
                    GameObject newEnemy = Instantiate(_enemyPrefabs[i], spawnPos, Quaternion.identity);
                    //set parent to container
                    newEnemy.transform.parent = enemyContainer.transform;
                    break;
                }
                else
                {
                    randomEnemyNumber -= _enemyWeightTable[i];
                }
                //wait for seconds
                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        foreach (var weight in _powerUpWeightTable)
        {
            _total += weight;
        }
        while (_stopSpawning == false)
        {
            //random spawn time
            float spawnIntervalPowerup = Random.Range(_minSpawnIntervalPowerup, _maxSpawnIntervalPowerup);
            //wait first
            yield return new WaitForSeconds(spawnIntervalPowerup);
            //random spawn pos
            Vector3 spawnPos = new Vector3(Random.Range(_minPosX, _maxPosX), _startPosY, 0);

            //spawn random powerup
            randomNumber = Random.Range(0, _total);
            //Debug.Log("total:" + total + " randomnumber:" + randomNumber);
            for (int i = 0; i < _powerUpWeightTable.Length; i++)
            {
                if (randomNumber <= _powerUpWeightTable[i])
                {
                    //Debug.Log(powerUps[i].name);                    
                    Instantiate(powerUpsPrefabs[i], spawnPos, Quaternion.identity);
                    break;
                }
                else
                {
                    randomNumber -= _powerUpWeightTable[i];
                }
            }
        }        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}