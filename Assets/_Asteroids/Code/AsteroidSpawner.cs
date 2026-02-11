using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid _prefab;

    private GameConfig _gameConfig;
    private CameraBounds _bounds;

    private void Update()
    {
        // TODO add button to test spawn.
    }

    public void Initialise(GameConfig gameConfig, CameraBounds bounds)
    {
        _gameConfig = gameConfig;
        _bounds = bounds;

        SpawnInitialAsteroids();
    }

    void SpawnInitialAsteroids()
    {
        for (int i = 0; i < _gameConfig.InitialAsteroidsCount; i++)
        {
            SpawnAsteroid();
        }
    }

    public void SpawnAsteroid()
    {
        // TODO use object pool.
        Asteroid asteroid = Instantiate(_prefab, _bounds.GetRandomBoundsPosition(), Quaternion.identity);
        
        asteroid.transform.SetParent(this.transform);
        
        asteroid.Initialise(_bounds);
    }
}