using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 3f;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyContainer;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        //null check enemy prefab
        if (enemyPrefab == null)
        {
            Debug.LogError("Spawnmanager.enemyprefab is NULL");
        }
        if (enemyPrefab == null)
        {
            Debug.LogError("Spawnmanager.enemycontainer is NULL");
        }

        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {        
                    
    }

    IEnumerator SpawnRoutine()
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

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
