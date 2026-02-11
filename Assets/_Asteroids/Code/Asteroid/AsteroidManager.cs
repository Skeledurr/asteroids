using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private Asteroid _prefab;

    private GameController _gameController;
    private CameraBounds _bounds;

    private uint _asteroidCount = 0;
    private uint _asteroidDestroyedCount = 0;
    private List<Asteroid> _activeAsteroids = new List<Asteroid>();

    public void Initialise(GameController gameController, CameraBounds bounds)
    {
        _gameController = gameController;
        _bounds = bounds;
    }

    public void SpawnRoundAsteroids(RoundSettings roundSettings)
    {
        ResetAsteroids();
        
        foreach (RoundSettings.SpawnSetting settings in roundSettings.AsteroidsToSpawn)
        {
            for (int i = 0; i < settings.Count; i++)
            {
                SpawnAsteroid(settings.AsteroidType, settings.BaseSpeedMultiplier);    
            }
        }
    }

    private void SpawnAsteroid(AsteroidType asteroidType, float baseSpeedMultiplier)
    {
        Asteroid asteroid = GameController.ObjectPool.Spawn<Asteroid>(
                                PoolMemberType.Asteroid_Size3, 
                            _bounds.GetRandomBoundsPosition(),
                                Quaternion.identity);

        asteroid.transform.name = $"{asteroidType} - id; {_asteroidCount}";
        asteroid.transform.SetParent(this.transform);
        
        asteroid.Initialise(this, baseSpeedMultiplier);
        
        _activeAsteroids.Add(asteroid);
        _asteroidCount++;
    }

    public void OnAsteroidDestroyed(Asteroid asteroid)
    {
        GameController.ObjectPool.Return(asteroid);
        
        _asteroidDestroyedCount++;

        if (_asteroidDestroyedCount == _asteroidCount)
        {
            _gameController.OnAllAsteroidsDestroyed();
        }
    }

    private void ResetAsteroids()
    {
        GameController.ObjectPool.ReturnAllOfType(PoolMemberType.Asteroid);

        _activeAsteroids.Clear();
        _asteroidCount = 0;
        _asteroidDestroyedCount = 0;
    }
}