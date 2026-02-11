using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Public Static
    
    public static CameraBounds CameraBounds => _cameraBounds;
    
    #endregion
    
    #region Serialized Fields
    
    [Header("Game Config")]
    [SerializeField] private GameConfig _gameConfig;
    
    [Header("Components")]
    [SerializeField] private Player _player;
    [SerializeField] private AsteroidManager _asteroidManager;
    
    #endregion

    #region Private Fields

    private static CameraBounds _cameraBounds;
    private GameSession _gameSession;

    #endregion
    
    #region Unity Methods
    
    private void Awake()
    {
        _cameraBounds = new CameraBounds(Camera.main, _gameConfig.CameraBoundsOffset);
        _asteroidManager.Initialise(this, _cameraBounds);
        _gameSession = new GameSession(_gameConfig.StartingRound, _gameConfig.StartingPlayerLives);
    }

    private void Start()
    {
        OnStartNextRound();
    }
    
    #endregion

    #region Game Events
    
    private void OnStartNextRound()
    {
        StartCoroutine(StartNexstRoundProcess());
    }

    public void OnAllAsteroidsDestroyed()
    {
        // Round Complete.
        StartCoroutine(EndRoundProcess());
    }

    public void OnPlayerDied()
    {
        // Check lives
        // Respawn?
    }
    
    #endregion

    #region Game Routines

    private IEnumerator StartNexstRoundProcess()
    {
        _player.SetControlsActive(false);
        
        _asteroidManager.SpawnRoundAsteroids(_gameConfig.GetRoundSettings(_gameSession.Round));

        yield return new WaitForSeconds(1f);
        
        _player.SetControlsActive(true);
    }

    private IEnumerator EndRoundProcess()
    {
        yield return new WaitForSeconds(1f);
        
        _gameSession.Round++;

        yield return StartNexstRoundProcess();
    }
    
    #endregion
}
