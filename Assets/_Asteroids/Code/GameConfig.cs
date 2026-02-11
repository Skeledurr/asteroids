using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Asteroids/Game Config", order = 0)]
public class GameConfig : ScriptableObject
{
    public float CameraBoundsOffset = 1f;

    public int InitialAsteroidsCount = 10;
}