using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

public class Spawner : ObjectPool
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Door _door;
    [SerializeField, Range(0f, 1f)] private float _fragmentDropChance = 0.3f; 
    [SerializeField] private Vector2Int _fragmentDropRange = new Vector2Int(1, 6); 

    private Player _player;
    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    private int _layer = 1;

    public event UnityAction AllEnemySpawned;
    public event UnityAction<int, int> EnemyCountChanged;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (ShouldSpawnEnemy())
        {
            SpawnEnemy();
            HandleEnemySpawned();
        }

        if (IsWaveComplete())
        {
            HandleWaveCompletion();
        }
    }

    private void HandleWaveCompletion()
    {
        if (_waves.Count > _currentWaveNumber + 1)
            AllEnemySpawned?.Invoke();

        _currentWave = null;
        _layer = 1;
    }

    private bool IsWaveComplete()
    {
        return _spawned >= _currentWave.Count;
    }

    private void HandleEnemySpawned()
    {
        _spawned++;
        _timeAfterLastSpawn = 0;

        EnemyCountChanged?.Invoke(_spawned, _currentWave.Count);
    }

    private bool ShouldSpawnEnemy()
    {
        return _timeAfterLastSpawn >= _currentWave.Delay && _spawned < _currentWave.Count;
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, _currentWave.Templates.Count);
        GameObject prefab = _currentWave.Templates[randomIndex];

        if (TryGetObject(out GameObject enemyObj) == false)
            enemyObj = CreateObject(prefab);

        enemyObj.transform.position = _spawnPoint.position;
        enemyObj.SetActive(true);

        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.GetComponent<SpriteRenderer>().sortingOrder = ++_layer;
        
        enemy.Init(_player, _door);
        enemy.ResetEnemy();
        
        enemy.Dying += OnEnemyDying;
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;

        _player.AddMoney(enemy.Reward);

        if (Random.value < _fragmentDropChance)
        {
            int fragments = Random.Range(_fragmentDropRange.x, _fragmentDropRange.y + 1);
            _player.AddFragments(fragments);
            Debug.Log($"Игрок получил {fragments} фрагментов!");
        }
    }


    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        _spawned = 0;
        _timeAfterLastSpawn = 0;
    }
}


[Serializable]
public class Wave
{
    public List<GameObject> Templates;
    public float Delay;
    public int Count;
}