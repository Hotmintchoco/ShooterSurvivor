using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public float[] spawnTime;

    LivingEntity playerEntity;
    Transform playerT;

    int level;
    float timer;
    bool isDisabled;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Start()
    {
        playerEntity = FindAnyObjectByType<Player>();
        playerT = playerEntity.transform;

        playerEntity.OnDeath += OnPlayerDeath;
    }

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

    void ResetPlayerPosition()
    {
        playerT.position = Vector3.zero + Vector3.up * 3;
    }

    void OnPlayerDeath()
    {
        isDisabled = true;
    }
}