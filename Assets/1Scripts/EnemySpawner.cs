using UnityEngine;

public class EnemySpawner : Spawner
{

    void Update()
    {
        if (!GameManager.instance.isLive) return;
        
        if (!isDisabled)
        {
            timer += Time.deltaTime;
            
            if (level < spawnTime.Length - 1)
                level = Mathf.FloorToInt(GameManager.instance.gameTime / 60f);

            if (timer > spawnTime[level])
            {
                timer = 0;
                Spawn();
            }
        }
    }

    void Spawn()
    {
        // 랜덤 몬스터 생성
        int minLevel = Mathf.Max(0, level - 2);
        int ranIndex = Random.Range(minLevel, level+1);
        
        GameObject enemy = GameManager.instance.pool.Get(ranIndex);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
    
}
