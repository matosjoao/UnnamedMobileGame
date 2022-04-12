using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private PlayerData _playerData;

    public List<Level> Levels { get { return _playerData.Levels; } }
    public int Score { get { return _playerData.Score; } }
    public int Lifes { get { return _playerData.Lifes; } }
    public int CurrentLevel { get { return _playerData.CurrentLevel; } }

    [Header("Data Save")]
    [SerializeField] private string saveFileName = "unnameddata.json";

    [Header("Levels Transaction")]
    [SerializeField] float transactionTime = 1.0f;

    private void Awake()
    {
        // Não destruir
        DontDestroyOnLoad(gameObject);
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
        _playerData.Levels.Find(level => level.LevelName == levelName).Complete();
    }

    public void CompleteLevel(string levelName, int starts)
    {
        _playerData.Levels.Find(level => level.LevelName == levelName).Complete(starts);
    }

    public void LockLevel(string levelName)
    {
        _playerData.Levels.Find(level => level.LevelName == levelName).Lock();
    }

    public void UnlockLevel(string levelName)
    {
        _playerData.Levels.Find(level => level.LevelName == levelName).Unlock();
    }

    public void UnlockLevel(int levelId)
    {
        _playerData.Levels.Find(level => level.ID == levelId).Unlock();
    }

    public Level GetCurrentLevel()
    {
        Level currentLevel = Levels.Find(level => level.ID == CurrentLevel);

        return currentLevel != null ? currentLevel : Levels.First();
    }

    public void OnCompleteLevel(int lifes, int score, string levelName)
    {
        // Atualizar Status
        _playerData.Lifes = lifes < 3 ? 3 : lifes;
        _playerData.Score += score;

        // Atualizar levels data
        CompleteLevel(levelName);
        int nextLevel = CurrentLevel == Levels.Count ? 1 : (CurrentLevel+1);
        UnlockLevel(nextLevel);
        _playerData.CurrentLevel = nextLevel;

        // Save Progress
        Save();

        // Load Menu
        LoadMenu(score);
    }

    IEnumerator LoadGame()
    {
        Load();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevel()
    {
        // Ir buscar o level actual
        Level level = GetCurrentLevel();

        // Iniciar level
        StartCoroutine(LoadGameLevel(level.LevelName));
    }

    public void LoadMenu(int levelScore)
    {
        StartCoroutine(LoadGameMenu());
    }

    IEnumerator LoadGameMenu()
    {
        // Iniciar transição
        HudManager.Instance.TriggerLevelTransaction();
        
        // Aguardar animação
        yield return new WaitForSeconds(transactionTime);
        // Carregar novo nível
        SceneManager.LoadScene("Menu");

        Debug.Log("1");
    }

    IEnumerator LoadGameLevel(string levelName)
    {
        // Iniciar transição
        MenuManager.Instance.TriggerLevelTransaction();
        // Aguardar animação
        yield return new WaitForSeconds(transactionTime);
        // Carregar novo nível
        SceneManager.LoadScene(levelName);

        Debug.Log("1");
    }
   

    #region Load/Save Player Status
    private void Save()
    {
        List<PlayerData> data = new List<PlayerData>();
        data.Add(_playerData);
        JsonSaving.SaveData<PlayerData>(data, saveFileName);
    }

    private void Load()
    {
        List<PlayerData> data = JsonSaving.LoadData<PlayerData>(saveFileName);
        _playerData = data.Count > 0 ? data[0] : null;
        if (_playerData == null)
        {
            List<Level> levels = new List<Level>
            {
                new Level(1, "Level1", false, 0, false),
                new Level(2, "Level2", false, 0, true),
                new Level(3, "Level3", false, 0, true),
            };

            _playerData = new PlayerData(3, 0, 1, levels);

            Save();
        }
    }

    private void StartNewGame()
    {
        List<Level> levels = new List<Level>
        {
            new Level(1, "Level1", false, 0, false),
            new Level(2, "Level2", false, 0, true),
            new Level(3, "Level3", false, 0, true),
        };

        _playerData = new PlayerData(3, 0, 1, levels);

        Save();
    }
    #endregion
}
