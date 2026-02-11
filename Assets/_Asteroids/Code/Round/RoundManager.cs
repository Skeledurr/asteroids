public class RoundManager
{
    public RoundManager(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
        _curRound = 0;
    }
    
    private GameConfig _gameConfig;
    private int _curRound;

    public void OnAllAsteroidsDestroyed()
    {
        
    }
}