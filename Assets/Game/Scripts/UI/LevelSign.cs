using UnityEngine;
using TMPro;

public class LevelSign : MonoBehaviour
{
    private GameController _gameController;

    [SerializeField] private TMP_Text levelNameField;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        if (_gameController)
        {
            Level level = _gameController.GetCurrentLevel();
            levelNameField.text = level.LevelName.ToUpper();
        }
    }
}
