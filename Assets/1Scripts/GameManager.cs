using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isLive;
    public float gameTime;
    public float maxGameTime = 3 * 9.9f;

    public int exp;
    public int[] nextExp = { 10, 30, 50, 80, 150, 300, 500, 750, 1000, 1500};
    public int maxExp;
    public int level;
    public int maxLevel;

    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;

    void Awake()
    {
        instance = this;
        maxExp = nextExp[nextExp.Length - 1];
        maxLevel = nextExp.Length - 1;
    }

    void Update()
    {
        if (!isLive) return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp(int _exp)
    {        
        exp += _exp;
        
        if (exp >= nextExp[level])
        {
            exp -= nextExp[level];

            if (level < maxLevel)
                level++;

            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        Cursor.visible = true;
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Cursor.visible = false;
        isLive = true;
        Time.timeScale = 1;
    }
}
