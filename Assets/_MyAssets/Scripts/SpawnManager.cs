using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    [SerializeField] private GameObject _targetContainer = default;
    [SerializeField] private GameObject _targetPrefab = default;
    [SerializeField] private GameObject _enemyPrefab = default;
    [SerializeField] private GameObject _tripleShotPrefab = default;
    [SerializeField] private GameObject _speedPrefab = default;
    [SerializeField] private GameObject _repairPrefab = default;
    [SerializeField] private GameObject _nukePrefab = default;
    
    [SerializeField] private GameObject _explosionPrefab;
    private GameObject[] _powerUpPrefab = default;
    private bool _stopSpawning = false;
    private GameManager _gameManager;
    private UiManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UiManager>();
        _powerUpPrefab = new GameObject[4];
        _powerUpPrefab[0] = _tripleShotPrefab;
        _powerUpPrefab[1] = _speedPrefab;
        _powerUpPrefab[2] = _repairPrefab;
        _powerUpPrefab[3] = _nukePrefab;
        StartSpawning();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnTargetRoutine());
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
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
            if(_gameManager.GetScore() >= 10 && _gameManager.GetGameStatus() < 3)
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

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);
        while(!_stopSpawning)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 14, 0);
            int randomPowerUp = Random.Range(0, _powerUpPrefab.Length);
            GameObject newPowerUp = Instantiate(_powerUpPrefab[randomPowerUp], spawnPosition, Quaternion.identity);
            newPowerUp.transform.parent = _targetContainer.transform;
            yield return new WaitForSeconds(Random.Range(10f, 30f)/_gameManager.GetSpawnMultiplier());
        }
    }

    public void Nuke()
    {
        foreach (Transform child in _targetContainer.transform)
        {
            if(child.tag == "Target" || child.tag == "Enemy" && child != null)
            {
                Instantiate(_explosionPrefab, child.transform.position, Quaternion.identity);
                Destroy(child.gameObject);
            }
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        _gameManager.GameOver();
    }
}
