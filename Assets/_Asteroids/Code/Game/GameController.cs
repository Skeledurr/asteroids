using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Game Config")]
    [SerializeField] private GameConfig _gameConfig;
    
    [Header("Components")]
    [SerializeField] private AsteroidManager _asteroidManager;

    private RoundManager _roundManager;
    private CameraBounds _cameraBounds;
    
    // Debug
    private float _nextRoundTimer = 0f;
    private float _roundTime = 5f;
    private int _curRound = 1;

    private void Awake()
    {
        _cameraBounds = new CameraBounds(Camera.main, _gameConfig.CameraBoundsOffset);
        _roundManager = new RoundManager(_gameConfig);
        _asteroidManager.Initialise(_cameraBounds, _roundManager);
    }

    private void Start()
    {
        _curRound = _gameConfig.StartingRound;
        StartRound(_curRound);
    }

    private void Update()
    {
        _nextRoundTimer += Time.deltaTime;
        if (_nextRoundTimer >= _roundTime)
        {
            _nextRoundTimer = 0f;
            _curRound++;
            StartRound(_curRound);
        }
    }

    private void StartRound(int round)
    {
        _asteroidManager.SpawnRoundAsteroids(_gameConfig.GetRoundSettings(round));
    }
}
