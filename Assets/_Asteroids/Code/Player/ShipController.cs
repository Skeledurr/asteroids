using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(WrapPosition))]
public class ShipController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _thrustPower = 10f;
    [SerializeField] private float _turnSpeed = 180f;

    [Header("Combat")]
    [SerializeField] private GameObject _bulletPrefab;
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
    }

    private void MoveUpdate()
    {
        float turn = _moveInput.x;
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0f, 0f, -turn * _turnSpeed * Time.fixedDeltaTime));

        _rigidbody.linearVelocity += transform.up * (_moveInput.y * _thrustPower * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        // TODO Object pool.
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }

    #endregion
}