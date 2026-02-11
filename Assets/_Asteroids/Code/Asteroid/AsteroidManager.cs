using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private AsteroidAtlas _atlas;
    
    private GameController _gameController;
    private GameBounds _bounds;
    private RoundSettings _curRoundSettings;
    private uint _asteroidCount = 0;

    public void Initialise(GameController gameController, GameBounds bounds)
    {
        _gameController = gameController;
        _bounds = bounds;
    }

    public void StartRound(RoundSettings roundSettings)
    {
        _curRoundSettings = roundSettings;
        
        ResetAsteroids();
        
        foreach (RoundSettings.SpawnSetting settings in _curRoundSettings.AsteroidsToSpawn)
        {
            for (int i = 0; i < settings.Count; i++)
            {
                SpawnAsteroid(settings.AsteroidType);    
            }
        }
    }

    private void SpawnAsteroid(AsteroidType asteroidType)
    {
        SpawnAsteroid(asteroidType, _bounds.GetRandomBoundsPosition());
    }
    
    public void SpawnAsteroid(AsteroidType asteroidType, Vector3 position)
    {
        AsteroidConfigData config = _atlas.GetAsteroidConfig(asteroidType);
        
        Asteroid asteroid = GameController.ObjectPool.Spawn<Asteroid>(
            config.PoolMemberType, 
            position,
            Quaternion.identity);

        asteroid.transform.name = $"{asteroidType} - id; {_asteroidCount}";
        asteroid.transform.SetParent(this.transform);
        
        asteroid.Initialise(this, config, _curRoundSettings.BaseSpeedMultiplier);
        
        _asteroidCount++;
    }

    public void OnAsteroidDestroyed(Asteroid asteroid)
    {
        GameController.ObjectPool.Return(asteroid);
        GameController.GameSession.OnAsteroidDestroyed(asteroid.PointValue);
        
        if (GameController.GameSession.AsteroidsDestroyedThisRound == _asteroidCount)
        {
            _gameController.OnAllAsteroidsDestroyed();
        }
    }

    private void ResetAsteroids()
    {
        GameController.ObjectPool.ReturnAllOfType(PoolMemberType.Asteroid);

        _asteroidCount = 0;
    }
}