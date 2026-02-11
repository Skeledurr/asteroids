using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Game Config")]
    [SerializeField] private GameConfig _gameConfig;
    
    [Header("Components")]
    [SerializeField] private AsteroidSpawner _asteroidSpawner;

    private CameraBounds _cameraBounds;

    private void Awake()
    {
        _cameraBounds = new CameraBounds(Camera.main, _gameConfig.CameraBoundsOffset);
        _asteroidSpawner.Initialise(_gameConfig, _cameraBounds);
    }
}
