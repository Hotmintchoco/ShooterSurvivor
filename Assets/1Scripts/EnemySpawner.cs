using UnityEngine;

public class EnemySpawner : Spawner
{

    void Update()
    {
        if (!isDisabled)
        {
            timer += Time.deltaTime;
            level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);

            if (timer > spawnTime[level])
            {
                timer = 0;
                Spawn();
            }
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(level);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
    
}
