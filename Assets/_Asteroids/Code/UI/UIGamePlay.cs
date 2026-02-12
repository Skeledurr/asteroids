using UnityEngine;
using TMPro;

/// <summary>
/// UI Game Play is a simple class that updates the Game Play UI
/// when Game Session values have been updated.
/// </summary>
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
