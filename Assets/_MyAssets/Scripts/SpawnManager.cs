using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _targetPrefab = default;
    [SerializeField] private GameObject _enemyPrefab = default;
    [SerializeField] private GameObject _targetContainer = default;
    private bool _stopSpawning = false;
    private GameManager _gameManager;
    private UiManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UiManager>();
        StartSpawning();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnTargetRoutine());
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnTargetRoutine()
    {
        yield return new WaitForSeconds(3f);
        while(!_stopSpawning)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newTarget = Instantiate(_targetPrefab, spawnPosition, Quaternion.identity);
            newTarget.transform.parent = _targetContainer.transform;
            yield return new WaitForSeconds(Random.Range(2f, 6f)/_gameManager.GetSpawnMultiplier());
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while(!_stopSpawning)
        {
            if(_gameManager.GetScore() >= 10)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.Euler(180, 0, 0));
                newEnemy.transform.parent = _targetContainer.transform;
                yield return new WaitForSeconds(Random.Range(10f, 20f)/_gameManager.GetSpawnMultiplier());
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        _gameManager.GameOver();
    }
}
