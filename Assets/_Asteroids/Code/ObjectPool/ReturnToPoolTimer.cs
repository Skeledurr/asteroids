using UnityEngine;

/// <summary>
/// A simple class that will automatically return the Pool Member
/// to the Object Pool after a duration.
/// This is mainly used by VFXs
/// </summary>
public class ReturnToPoolTimer : MonoBehaviour
{
    [SerializeField] private float _duration;

    private PoolMember _poolMember;
    private float _curTimer;

    private void Awake()
    {
        _poolMember = this.GetComponent<PoolMember>();
    }

    private void OnEnable()
    {
        _curTimer = _duration;
    }

    private void Update()
    {
        _curTimer -= Time.deltaTime;
        if (_curTimer < 0)
        {
            _poolMember.ReturnToPool();
        }
    }
}
