using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData 
{
    public int Lifes;
    public int Score;
    public int CurrentLevel;
    public List<Level> Levels;

    public PlayerData(int lifes, int score, int currentLevel, List<Level> levels)
    {
        Lifes = lifes;
        Score = score;
        CurrentLevel = currentLevel;
        Levels = levels;
    }
}
