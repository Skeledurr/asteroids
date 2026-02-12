using System;
using UnityEngine;

/// <summary>
/// UI Slow Motion bar is simple class that
/// will adjust the size of the bar displaying how
/// much duration remains for slow motion.
/// </summary>
public class UISlowMotion : MonoBehaviour
{
    [SerializeField] private RectTransform _slowMoBar;
    [SerializeField] private float _sloMoBarMaxWidth = 430f;

    private void Update()
    {
        SetBarWidth();
    }

    private void SetBarWidth()
    {
        float percent = GameController.SlowMotion.SlowMotionTimePercent;
        float width = percent * _sloMoBarMaxWidth;
        Vector2 size = _slowMoBar.sizeDelta;
        size.x = width;
        _slowMoBar.sizeDelta = size;
    }
}