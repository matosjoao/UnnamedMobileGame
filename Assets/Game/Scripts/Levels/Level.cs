using System;

[Serializable]
public class Level
{
    public int ID;
    public string LevelName;
    public bool Completed;
    public int Stars;
    public bool Locked;

    public Level(int id, string levelName, bool completed, int stars, bool locked)
    {
        this.ID = id;
        this.LevelName = levelName;
        this.Completed = completed;
        this.Stars = stars;
        this.Locked = locked;
    }

    public void Complete()
    {
        this.Completed = true;
    }

    public void Complete(int stars)
    {
        this.Completed = true;
        this.Stars = stars;
    }

    public void Lock()
    {
        this.Locked = true;
    }

    public void Unlock()
    {
        this.Locked = false;
    }
}
