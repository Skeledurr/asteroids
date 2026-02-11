using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private Asteroid _prefab;

    private CameraBounds _bounds;
    private RoundManager _roundManager;

    private List<Asteroid> _activeAsteroids = new List<Asteroid>();

    public void Initialise(CameraBounds bounds, RoundManager roundManager)
    {
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
        
        asteroid.transform.SetParent(this.transform);
        
        asteroid.Initialise(_bounds, baseSpeedMultiplier);
        
        _activeAsteroids.Add(asteroid);
    }

    private void OnAsteroidDestroyed(Asteroid asteroid)
    {
        // TODO Reduce count.
        // TODO Unsubscribe from event.
        // TODO Check if round complete.
    }

    private void ResetAsteroids()
    {
        // TODO destroy without notifying _activeAsteroids
        
        for (int i = 0; i < _activeAsteroids.Count; i++)
        {
            // TODO inform the asteroid to destroy (do not notify, do not create children)
            // Asteroid or manager handles object pool.
            Destroy(_activeAsteroids[i].gameObject);
        }

        _activeAsteroids.Clear();
    }
}