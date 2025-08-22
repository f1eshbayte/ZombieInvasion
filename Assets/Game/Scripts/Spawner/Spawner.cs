using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Spawner : ObjectPool
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;
    [SerializeField] private Door _door;

    
    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    private int _layer = 1;

    public event UnityAction AllEnemySpawned;

    public event UnityAction<int, int> EnemyCountChanged;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;
        
        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
            EnemyCountChanged?.Invoke(_spawned, _currentWave.Count);
        }

        if (_currentWave.Count <= _spawned)
        {
            if (_waves.Count > _currentWaveNumber + 1)
                AllEnemySpawned?.Invoke();

            _currentWave = null;
        }
        
    }
    
    private void InstantiateEnemy()
    {
        var randomEnemy = Random.Range(0, _currentWave.Templates.Count);
        
        Enemy enemy = Instantiate(_currentWave.Templates[randomEnemy], _spawnPoint.position, _spawnPoint.rotation, _spawnPoint)
            .GetComponent<Enemy>();
        
        enemy.GetComponent<SpriteRenderer>().sortingOrder = ++_layer;
        enemy.Init(_player, _door);
        
        enemy.Dying += OnEnemyDying;
    }
    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;

        _player.AddMoney(enemy.Reward);
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }
}

[Serializable]
public class Wave
{
    public List<GameObject> Templates;
    public float Delay;
    public int Count;
}