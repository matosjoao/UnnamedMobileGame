using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private List<Level> _levels;
    private int _score;
    private int _lifes;

    public List<Level> Levels { get { return _levels; } }
    public int Score { get { return _score; } }
    public int Lifes { get { return _lifes; } }

    private void Awake()
    {
        // Não destruir
        DontDestroyOnLoad(gameObject);

        // Inicializar Levels
        _levels = new List<Level>
        {
            new Level(1, "Level1", false, 0, false),
            new Level(2, "Level2", false, 0, true),
            new Level(3, "Level3", false, 0, true),
        };
    }

    private void Start()
    {
        StartCoroutine(LoadGame());
    }

    public void StartLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void CompleteLevel(string levelName)
    {
        _levels.Find(level => level.LevelName == levelName).Complete();
    }

    public void CompleteLevel(string levelName, int starts)
    {
        _levels.Find(level => level.LevelName == levelName).Complete(starts);
    }

    public void LockLevel(string levelName)
    {
        _levels.Find(level => level.LevelName == levelName).Lock();
    }

    public void UnlockLevel(string levelName)
    {
        _levels.Find(level => level.LevelName == levelName).Unlock();
    }

    public Level GetCurrentLevel()
    {
        Level currentLevel = _levels.Find(level => level.Completed == false);

        return currentLevel != null ? currentLevel : _levels.First();
    }

    public void UpdatePlayerStatus(int lifes, int score)
    {
        // Atualizar Status
        _lifes = lifes;
        _score += score;

        // Save Progress
        Save();
    }

    IEnumerator LoadGame()
    {
        Load();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Menu");
    }

    #region Load/Save Player Status
    private void Save()
    {
        PlayerPrefs.SetInt(GameControllerKeys.Score, _score);
        PlayerPrefs.SetInt(GameControllerKeys.Lifes, _lifes);
    }

    private void Load()
    {
        _score = PlayerPrefs.GetInt(GameControllerKeys.Score, 0);
        _lifes = PlayerPrefs.GetInt(GameControllerKeys.Lifes, 3);
    }

    private void StartNewGame()
    {
        PlayerPrefs.DeleteKey(GameControllerKeys.Score);
        PlayerPrefs.SetInt(GameControllerKeys.Lifes, 3);
    }
    #endregion
}

public static class GameControllerKeys
{
    public const string Score = "sc0r3";
    public const string Lifes = "l1f3s";
}

