using System;
using UnityEngine;

/// <summary>
/// Slow Motion handles the state of the player's slow motion tool.
/// Maintaining the current duration and recharging.
/// </summary>
public class SlowMotion : MonoBehaviour
{
    public float SlowMotionTimePercent => Mathf.Clamp01(_curTime / _playerConfig.SlowMotionDuration);
    
    [SerializeField] private PlayerConfig _playerConfig;
    
    private bool _slowMoActive = false;
    private float _curTime = 0f;
    
    private void Update()
    {
        UpdateTimer();
    }

    public void PrepareRoundStart()
    {
        ResetSlowMotion();
        EndSlowMotion();
    }

    public void RoundComplete()
    {
        EndSlowMotion();
    }

    public void StartSlowMotion()
    {
        if (_slowMoActive || _curTime < 0.5f) return;
        
        _slowMoActive = true;
        GameController.GameTime.SetSlowMotion(_playerConfig.SlowMotionPercent);
    }

    public void EndSlowMotion()
    {
        if (!_slowMoActive) return;
        
        _slowMoActive = false;
        GameController.GameTime.ResetSlowMotion();
    }

    private void UpdateTimer()
    {
        if (_slowMoActive)
        {
            _curTime -= Time.deltaTime;
            if (_curTime <= 0f)
            {
                EndSlowMotion();
            }
        }
        else
        {
            _curTime += Time.deltaTime * _playerConfig.SlowMotionRechargeRate;
        }
    }

    private void ResetSlowMotion()
    {
        _curTime = _playerConfig.SlowMotionDuration;
    }
}