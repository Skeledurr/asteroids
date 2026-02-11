public class GameSession
{
    public GameSession(int startingRound, int playerLives)
    {
        Round = startingRound;
        PlayerLives = playerLives;
        Score = 0;
    }
    
    public int Round { get; private set; }
    public int Score { get; private set; }
    public int PlayerLives { get; private set; }
    
    public int AsteroidsDestroyedThisRound { get; private set; }
    public int TotalAsteroidsDestroyed { get; private set; }
    
    public bool IsGameOver => PlayerLives <= 0;
    
    public void AddScore(int amount)
    {
        Score += amount;
    }

    public void LoseLife()
    {
        PlayerLives--;
    }

    public void NextRound()
    {
        Round++;
        AsteroidsDestroyedThisRound = 0;
    }

    public void OnAsteroidDestroyed(int scoreValue)
    {
        AsteroidsDestroyedThisRound++;
        TotalAsteroidsDestroyed++;
        AddScore(scoreValue);
    }
}

