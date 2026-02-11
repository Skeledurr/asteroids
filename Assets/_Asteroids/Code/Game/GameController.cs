using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Public Static
    
    public static CameraBounds CameraBounds => _cameraBounds;
    public static ObjectPool ObjectPool { get; private set; }

    #endregion
    
    #region Serialized Fields
    
    [Header("Game Config")]
    [SerializeField] private GameConfig _gameConfig;
    
    [Header("Components")]
    [SerializeField] private Player _player;
    [SerializeField] private AsteroidManager _asteroidManager;
    [SerializeField] private ObjectPool _objectPool;
    
    #endregion

    #region Private Fields

    private static CameraBounds _cameraBounds = new ();
    private GameSession _gameSession;

    #endregion
    
    #region Unity Methods
    
    private void Awake()
    {
        ObjectPool = _objectPool;
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
        _player.ResetPlayer();
        
        _asteroidManager.StartRound(_gameConfig.GetRoundSettings(_gameSession.Round));

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
