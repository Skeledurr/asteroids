using UnityEngine;

/// <summary>
/// Ship Controller uses the user's input to move and fire the ship.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(WrapPosition))]
public class ShipController : MonoBehaviour
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private Transform _firePoint;
    
    private Rigidbody _rigidbody;
    private PlayerControls _controls;
    private Vector2 _moveInput;

    #region Unity Methods
    
    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        InitialiseInput();
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }
    
    private void FixedUpdate()
    {
        MoveUpdate();
    }
    
    #endregion
    
    #region Public Methods
    
    public void ResetShip()
    {
        _moveInput = Vector2.zero;
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.position = Vector3.zero;
        _rigidbody.rotation = Quaternion.identity;
        StopSlowMotion();
    }
    
    #endregion
    
    #region Private Methods

    private void InitialiseInput()
    {
        // Instantiate the generated input class
        _controls = new PlayerControls();

        // Subscribe to events
        _controls.Player.Move.performed += cont => _moveInput = cont.ReadValue<Vector2>();
        _controls.Player.Move.canceled += cont => _moveInput = Vector2.zero;

        _controls.Player.Attack.performed += cont => Shoot();

        _controls.Player.Interact.performed += cont => StartSlowMotion();
        _controls.Player.Interact.canceled += cont => StopSlowMotion();
    }

    private void MoveUpdate()
    {
        float turn = _moveInput.x;
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0f, 0f, -turn * _playerConfig.ShipTurnSpeed * Time.fixedDeltaTime));

        _rigidbody.linearVelocity += transform.up * (_moveInput.y * _playerConfig.ShipThrustPower * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        GameController.ObjectPool.Spawn(_playerConfig.BulletType, _firePoint.position, _firePoint.rotation);
    }

    private void StartSlowMotion()
    {
        GameController.SlowMotion.StartSlowMotion();
    }

    private void StopSlowMotion()
    {
        GameController.SlowMotion.EndSlowMotion();
    }

    #endregion
}