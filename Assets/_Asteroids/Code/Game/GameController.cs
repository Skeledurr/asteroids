using UnityEngine;

/// <summary>
/// Game Controller oversees the overall game lifecycle. Initialising core components,
/// tracks the active session, and coordinates transitions between start,
/// gameplay, round changes, and game over.
/// </summary>
public class GameController : MonoBehaviour
{
    #region Public Static
    
    public static GameBounds GameBounds => _gameBounds;
    public static ObjectPool ObjectPool { get; private set; }
    public static GameSession GameSession => _gameSession;
    public static SlowMotion SlowMotion { get; private set; }
    public static GameTime GameTime => _gameTime;
    
    #endregion
    
    #region Serialized Fields
    
    [Header("Game Config")]
    [SerializeField] private GameConfig _gameConfig;
    
    [Header("Components")]
    [SerializeField] private AsteroidManager _asteroidManager;
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private RoundController _roundController;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private SlowMotion _slowMotion;
    
    #endregion

    #region Private Fields

    private static GameBounds _gameBounds = new ();
    private static GameSession _gameSession = new();
    private static GameTime _gameTime = new();

    #endregion
    
    #region Unity Methods
    
    private void Awake()
    {
        ObjectPool = _objectPool;
        SlowMotion = _slowMotion;
        _gameBounds = new GameBounds(Camera.main, _gameConfig.CameraBoundsOffset);
        _asteroidManager.Initialise(_gameBounds);
        _roundController.Initialise(this, _gameConfig);
        _uiManager.Initialise(this);
    }

    private void Start()
    {
        _uiManager.SetUIState(UIManager.UIState.StartGame);
    }

    #endregion

    #region Public Methods

    public void StartNewGame()
    {
        _roundController.StartNewGame();
        _uiManager.SetUIState(UIManager.UIState.GamePlay);
    }

    public void RoundComplete()
    {
        _roundController.StartRound();
        _uiManager.SetUIState(UIManager.UIState.GamePlay);
    }

    public void GameOver()
    {
        _uiManager.SetUIState(UIManager.UIState.GameOver);
    }

    #endregion
    
}
