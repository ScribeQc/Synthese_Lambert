using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _targetPrefab = default;
    [SerializeField] private GameObject _targetContainer = default;
    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnTargetRoutine());
    }

    IEnumerator SpawnTargetRoutine()
    {
        yield return new WaitForSeconds(3f);
        while(!_stopSpawning)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newTarget = Instantiate(_targetPrefab, spawnPosition, Quaternion.identity);
            newTarget.transform.parent = _targetContainer.transform;
            yield return new WaitForSeconds(Random.Range(2f, 6f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
