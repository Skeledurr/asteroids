using UnityEngine;

public class GameSession
{
    public int Round { get; private set; }
    public int Score { get; private set; }
    public int PlayerLives { get; private set; }
    public bool IsPlayerAlive { get; private set; }
    
    public int AsteroidsSpawnedThisRound { get; private set; }
    public int AsteroidsDestroyedThisRound { get; private set; }
    public int TotalAsteroidsDestroyed { get; private set; }

    public bool IsGameOver => PlayerLives <= 0;
    public bool IsRoundOver => AsteroidsSpawnedThisRound > 0 && 
                               (AsteroidsSpawnedThisRound == AsteroidsDestroyedThisRound ||
                               !IsPlayerAlive);

    public event System.Action OnValuesUpdated;

    public void StartNewSession(GameConfig config)
    {
        Round = config.StartingRound;
        PlayerLives = config.StartingPlayerLives;
        Score = 0;
        AsteroidsDestroyedThisRound = 0;
        AsteroidsSpawnedThisRound = 0;
        OnValuesUpdated?.Invoke();
    }
    
    public void AddScore(int amount)
    {
        Score += amount;
        OnValuesUpdated?.Invoke();
    }

    public void PlayerAlive()
    {
        IsPlayerAlive = true;
    }

    public void PlayerDied()
    {
        PlayerLives--;
        IsPlayerAlive = false;
        OnValuesUpdated?.Invoke();
    }

    public void RoundComplete()
    {
        AsteroidsDestroyedThisRound = 0;
        AsteroidsSpawnedThisRound = 0;
    }

    public void NextRound()
    {
        Round++;
        OnValuesUpdated?.Invoke();
     }

    public void OnAsteroidSpawned()
    {
        AsteroidsSpawnedThisRound++;
    }

    public void OnAsteroidDestroyed(int scoreValue)
    {
        AsteroidsDestroyedThisRound++;
        TotalAsteroidsDestroyed++;
        AddScore(scoreValue);
    }
}

