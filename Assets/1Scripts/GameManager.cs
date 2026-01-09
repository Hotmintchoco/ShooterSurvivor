using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime;
    public float maxGameTime = 3 * 9.9f;

    public int exp;
    public int[] nextExp = { 10, 30, 50, 80, 150, 300, 500, 750, 1000, 1500};
    public int level;

    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
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
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }
}
