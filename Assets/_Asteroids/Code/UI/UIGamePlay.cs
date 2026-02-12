using System;
using UnityEngine;
using TMPro;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private TextMeshProUGUI _roundsText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        UpdateText();
        GameController.GameSession.OnValuesUpdated += UpdateText;
    }

    private void OnDisable()
    {
        GameController.GameSession.OnValuesUpdated -= UpdateText;
    }

    private void UpdateText()
    {
        _livesText.text = $"Lives: {GameController.GameSession.PlayerLives}";
        _roundsText.text = $"Round: {GameController.GameSession.Round}";
        _scoreText.text = $"Score: {GameController.GameSession.Score}";
    }
}
