using UnityEngine;

/// <summary>
/// Game Config has a scriptable object container that holds numerous setting
/// values that can be adjusted to change game play.
/// </summary>
[CreateAssetMenu(fileName = "GameConfig", menuName = "Asteroids/Game Config", order = 0)]
public class GameConfig : ScriptableObject
{
    public float CameraBoundsOffset => _cameraBoundsOffset;
    public int StartingRound => _startingRound;
    public int StartingPlayerLives => _startingPlayerLives;
    
    [Tooltip("Extra padding added inside the screen edges when calculating gameplay bounds.")]
    [SerializeField] private float _cameraBoundsOffset = 1f;
    
    [Tooltip("Round the game starts on. Useful for testing specific rounds.")]
    [SerializeField] private int _startingRound = 1;
    
    [Tooltip("The amount of lives the player has a the start of a game")]
    [SerializeField] private int _startingPlayerLives = 3;
    
    [SerializeField] private GameRoundsConfig _gameRoundsConfig;

    public RoundSettings GetRoundSettings(int round)
    {
        return _gameRoundsConfig.GetRoundSettings(round);
    }
}