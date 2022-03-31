using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameController _gameController;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        if(_gameController != null)
        {
            int score = _gameController.Score;
            int lifes = _gameController.Lifes;

            HudManager.Instance.SetCoins(score.ToString());
            HudManager.Instance.SetLifes(lifes.ToString());
        }
    }
}
