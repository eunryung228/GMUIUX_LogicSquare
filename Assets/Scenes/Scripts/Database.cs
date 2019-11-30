using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Database : MonoBehaviour
{
    public List<Level> m_levels = new List<Level>();

    private string level;
    private int round;

    static public Database instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }


    public void SetData(string lev, int r)
    {
        level = lev;
        round = r;
    }

    public string GetLevel()
    {
        return level;
    }
    public int GetRoundNum()
    {
        return round;
    }

    void Start()
    {
        List<int> compArray = new List<int>();
        compArray.Add(0); compArray.Add(0); compArray.Add(0);
        Level lev_easy = new Level("Easy", compArray);
        Level lev_normal = new Level("Normal", compArray);
        Level lev_hard = new Level("Hard", compArray);
        m_levels.Add(lev_easy);
        m_levels.Add(lev_normal);
        m_levels.Add(lev_hard);
    }

    void Update()
    {
    }

    [SerializeField]
    public class Level
    {
        public string levelName;
        public List<int> isComplete = new List<int>();

        public Level(string level, List<int> comp)
        {
            this.levelName = level;
            this.isComplete = comp;
        }

        public void SetLevel(string level)
        {
            this.levelName = level;
        }
        public void SetComp(int num, int h)
        {
            this.isComplete[num] = h;
        }
    }
}
