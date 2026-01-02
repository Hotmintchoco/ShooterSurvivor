using UnityEngine;

public class BoxSpawner : Spawner
{
    void Update()
    {
        if (!isDisabled)
        {
            timer += Time.deltaTime;

            if (timer > spawnTime[0])
            {
                timer = 0;
                Spawn();
            }
        }
    }

    void Spawn()
    {
        GameObject box = GameManager.instance.pool.GetBox();
        box.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}
