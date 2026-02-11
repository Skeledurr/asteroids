using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    #region Public Static
    
    public static GameBounds GameBounds => _gameBounds;
    public static ObjectPool ObjectPool { get; private set; }
    public static GameSession GameSession => _gameSession;

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

    private static GameBounds _gameBounds = new ();
    private static GameSession _gameSession;
    private PlayerControls _controls;
    private Coroutine _currentRoutine = null;

    #endregion
    
    #region Unity Methods
    
    private void Awake()
    {
        ObjectPool = _objectPool;
        _gameBounds = new GameBounds(Camera.main, _gameConfig.CameraBoundsOffset);
        _asteroidManager.Initialise(this, _gameBounds);
        _gameSession = new GameSession(_gameConfig.StartingRound, _gameConfig.StartingPlayerLives);
        _player.Initialise(this);
        _controls = new PlayerControls();
        _controls.UI.Submit.performed += OnStartNewGame;
        _controls.UI.Disable();
    }

    private void Start()
    {
        OnStartNextRound();
    }
    
    #endregion

    #region Game Events
    
    private void OnStartNextRound()
    {
        if (_currentRoutine != null) return;
        
        _currentRoutine = StartCoroutine(StartRoundProcess());
    }

    public void OnAllAsteroidsDestroyed()
    {
        if (_currentRoutine != null) return;
        
        _currentRoutine = StartCoroutine(EndRoundProcess());
    }

    public void OnPlayerDied()
    {
        if (_currentRoutine != null) return;
        
        _currentRoutine = StartCoroutine(PlayerDeathProcess());
    }
    
    #endregion

    #region Game Routines

    private IEnumerator StartRoundProcess()
    {
        _player.gameObject.SetActive(true);
        _player.ResetPlayer();
        
        _asteroidManager.StartRound(_gameConfig.GetRoundSettings(_gameSession.Round));

        yield return new WaitForSeconds(1f);
        
        _player.SetControlsActive(true);

        _currentRoutine = null;
    }

    private IEnumerator EndRoundProcess()
    {
        yield return new WaitForSeconds(1f);
        
        GameSession.NextRound();

        yield return StartRoundProcess();

        _currentRoutine = null;
    }

    private IEnumerator PlayerDeathProcess()
    {
        _player.ResetPlayer();
        _player.gameObject.SetActive(false);
        GameSession.LoseLife();
        Debug.Log($"Lives Remaining: {_gameSession.PlayerLives}");
        
        yield return new WaitForSeconds(1f);

        if (_gameSession.IsGameOver)
        {
            Debug.Log("Game Over. Press Enter to Start Again.");
            
            _controls.UI.Enable();
        }
        else
        {
            yield return StartRoundProcess();
        }

        _currentRoutine = null;
    }

    private void OnStartNewGame(InputAction.CallbackContext obj)
    {
        if (_currentRoutine != null)
        {
            return;
        }
        
        _controls.UI.Disable();
        
        _gameSession = new GameSession(_gameConfig.StartingRound, _gameConfig.StartingPlayerLives);
        
        _currentRoutine = StartCoroutine(StartRoundProcess());
    }

    #endregion
}
