using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private int _delay;
    [SerializeField] private Coin _template;

    private WaitForSeconds _delayBetweenSpawn;

    private SpawnPoint[] _spawnPoints;

    private int _currentPoint;    

    private void Start()
    {
        _delayBetweenSpawn = new WaitForSeconds(_delay);
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        StartCoroutine(SpawnCoin());
    }

    private IEnumerator SpawnCoin()
    {
        while (true)
        {
            _currentPoint = Random.Range(0, _spawnPoints.Length);

            Instantiate(_template, _spawnPoints[_currentPoint].transform.position, Quaternion.identity);

            yield return _delayBetweenSpawn;
        }
    }
}
