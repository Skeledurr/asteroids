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
        // TODO use object pool with type.
        
        Asteroid asteroid = Instantiate(_prefab, _bounds.GetRandomBoundsPosition(), Quaternion.identity);

        asteroid.transform.name = $"{asteroidType} - id; {_asteroidCount}";
        asteroid.transform.SetParent(this.transform);
        
        asteroid.Initialise(this, baseSpeedMultiplier);
        
        _activeAsteroids.Add(asteroid);
        _asteroidCount++;
    }

    public void OnAsteroidDestroyed(Asteroid asteroid)
    {
        // TODO use object pool.
        Destroy(asteroid.gameObject);
        
        _asteroidDestroyedCount++;

        if (_asteroidDestroyedCount == _asteroidCount)
        {
            _gameController.OnAllAsteroidsDestroyed();
        }
    }

    private void ResetAsteroids()
    {
        // TODO destroy without notifying _activeAsteroids
        
        
        // TODO Clear out all active asteroids via Object pool.
        for (int i = 0; i < _activeAsteroids.Count; i++)
        {
            if (_activeAsteroids[i] == null) continue;
            // TODO inform the asteroid to destroy (do not notify, do not create children)
            // Asteroid or manager handles object pool.
            Destroy(_activeAsteroids[i].gameObject);
        }

        _activeAsteroids.Clear();
        _asteroidCount = 0;
        _asteroidDestroyedCount = 0;
    }
}