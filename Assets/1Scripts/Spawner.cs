using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public float[] spawnTime;

    LivingEntity playerEntity;

    protected int level;
    protected float timer;
    protected bool isDisabled;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    public virtual void Start()
    {
        playerEntity = FindAnyObjectByType<Player>();

        playerEntity.OnDeath += OnPlayerDeath;
        timer = spawnTime[0];
    }

    void OnPlayerDeath()
    {
        isDisabled = true;
    }

    
}