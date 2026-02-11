public class GameSession
{
    public GameSession(int startingRound, int playerLives)
    {
        Round = startingRound;
        PlayerLives = playerLives;
        Score = 0;
    }
    
    public int Round;
    public int Score;
    public int PlayerLives;
}