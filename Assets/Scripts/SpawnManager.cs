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

    //wave config
    private int _waveNumber = 0;
    private int _enemiesToSpawn = 0;
    [SerializeField] private int _ratioWaveEnemies = 1;
    private int _enemiesLeft = 0;

    //handles
    private UIManager _uIManager;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        _uIManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        if (_uIManager == null)
        { Debug.LogError("SpawnManager.uimanger is NULL"); }

        if (enemyContainer == null)
        { Debug.LogError("Spawnmanager.enemycontainer is NULL"); }
        //TODO null check GameObject[] x2?

        NextWave();
    }

    private void NextWave()
    {
        //start next wave
            _waveNumber++;
            _uIManager.UpdateUIWave(_waveNumber);

            _enemiesToSpawn = _waveNumber * _ratioWaveEnemies;
            _enemiesLeft = _enemiesToSpawn;
            _uIManager.UpdateUIEnemiesLeft(_enemiesLeft);

            //decrease spawn interval each wave
            _spawnInterval *= 0.9f;

            StartSpawning();        
    }

    public void DecreaseEnemiesLeft()
    {
        _enemiesLeft--;
        _uIManager.UpdateUIEnemiesLeft(_enemiesLeft);
        if (_enemiesLeft <= 0)
        {
            NextWave();
        }
    }


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        //yield return new WaitForSeconds(3f);

        foreach (var weight in _enemyWeightTable)
        {
            _enemyWeightTotal += weight;
        }

        //test
        Debug.Log("spawn enemy routine started");

        //while player is alive
        while (_enemiesToSpawn > 0 && _stopSpawning == false)
        {
            //spawn enemy
            randomEnemyNumber = Random.Range(0, _enemyWeightTotal);

            for (int i = 0; i < _enemyWeightTable.Length; i++)
            {
                if (randomEnemyNumber <= _enemyWeightTable[i])
                {
                    Vector3 spawnPos = new Vector3(Random.Range(_minPosX, _maxPosX), _startPosY, 0);
                    GameObject newEnemy = Instantiate(_enemyPrefabs[i], spawnPos, Quaternion.identity);
                    Debug.Log("SpawnManager: spawining enemy of type: "+_enemyPrefabs[i]);
                    _enemiesToSpawn--;
                    //set parent to container
                    newEnemy.transform.parent = enemyContainer.transform;                    
                    break;
                }
                else
                {
                    randomEnemyNumber -= _enemyWeightTable[i];
                }
            }
            //Debug.Log("enemies to spawn: " + _enemiesToSpawn);
            _uIManager.UpdateEnemiesToSpawn(_enemiesToSpawn);
            yield return new WaitForSeconds(_spawnInterval);            
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        foreach (var weight in _powerUpWeightTable)
        {
            _total += weight;
        }
        while (_enemiesLeft > 0 && _stopSpawning == false)
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