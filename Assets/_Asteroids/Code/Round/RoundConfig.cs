using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundConfig", menuName = "Asteroids/Round Config", order = 0)]
public class RoundConfig : ScriptableObject
{
    public RoundSettings Settings;
}