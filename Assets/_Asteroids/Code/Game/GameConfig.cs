using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Asteroids/Game Config", order = 0)]
public class GameConfig : ScriptableObject
{
    public float CameraBoundsOffset => _cameraBoundsOffset;
    public int StartingRound => _startingRound;
    public int StartingPlayerLives => _startingPlayerLives;
    
    [SerializeField] private float _cameraBoundsOffset = 1f;
    [SerializeField] private int _startingRound = 1;
    [SerializeField] private int _startingPlayerLives = 3;
    [SerializeField] private GameRoundsConfig _gameRoundsConfig;

    public RoundSettings GetRoundSettings(int round)
    {
        return _gameRoundsConfig.GetRoundSettings(round);
    }
}