using UnityEngine;

/// <summary>
/// Asteroid Manager handles the spawning and destroying of Asteroids
/// during a game. 
/// </summary>
public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private AsteroidAtlas _atlas;
    
    private GameBounds _bounds;
    private RoundSettings _curRoundSettings;

    public void Initialise(GameBounds bounds)
    {
        _bounds = bounds;
    }

    public void PrepareRoundStart()
    {
        ResetAsteroids();
    }

    public void StartRound(RoundSettings roundSettings)
    {
        _curRoundSettings = roundSettings;
        
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

        GameController.GameSession.OnAsteroidSpawned();
        
        asteroid.transform.name = $"{asteroidType} - round: {GameController.GameSession.Round}, id; {GameController.GameSession.AsteroidsSpawnedThisRound}";
        asteroid.transform.SetParent(this.transform);
        
        asteroid.Initialise(this, config, _curRoundSettings.BaseSpeedMultiplier);
    }

    public void OnAsteroidDestroyed(Asteroid asteroid)
    {
        GameController.GameSession.OnAsteroidDestroyed(asteroid.PointValue);
        GameController.ObjectPool.Return(asteroid);
    }

    private void ResetAsteroids()
    {
        GameController.ObjectPool.ReturnAllOfType(PoolMemberType.Asteroid);
    }
}