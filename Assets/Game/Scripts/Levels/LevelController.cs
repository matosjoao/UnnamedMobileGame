using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private List<Level> _levels;

    public List<Level> Levels { get { return _levels; } }

    private void Awake()
    {
        // Não destruir
        DontDestroyOnLoad(gameObject);

        // Inicializar Levels
        _levels = new List<Level>
        {
            new Level(0, "SampleScene", false, 0, false),
            new Level(1, "SampleScene", false, 2, true),
            new Level(2, "SampleScene", false, 0, true),
            new Level(3, "SampleScene", false, 0, true),
            new Level(4, "SampleScene", false, 1, true),
            new Level(5, "SampleScene", false, 0, true),
            new Level(6, "SampleScene", false, 0, true),
            new Level(7, "SampleScene", false, 0, true),
            new Level(8, "SampleScene", false, 0, true),
            new Level(9, "SampleScene", false, 0, true),
            new Level(10, "SampleScene", false, 0, true),
            new Level(11, "SampleScene", false, 0, true),
            new Level(12, "SampleScene", false, 0, true),
            new Level(13, "SampleScene", false, 0, true),
            new Level(14, "SampleScene", false, 2, true),
            new Level(15, "SampleScene", false, 0, true),
            new Level(16, "SampleScene", false, 0, true),
            new Level(17, "SampleScene", false, 0, true),
            new Level(18, "SampleScene", false, 0, true),
            new Level(19, "SampleScene", false, 0, true),
            new Level(20, "SampleScene", false, 0, true),
            new Level(21, "SampleScene", false, 0, true),
            new Level(22, "SampleScene", false, 0, true),
            new Level(23, "SampleScene", false, 0, true),
            new Level(24, "SampleScene", false, 3, true),
            new Level(25, "SampleScene", false, 0, true),
            new Level(26, "SampleScene", false, 0, true),
            new Level(27, "SampleScene", false, 0, true)
        };
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
}
