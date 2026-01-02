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

    void Start()
    {
        playerEntity = FindAnyObjectByType<Player>();

        playerEntity.OnDeath += OnPlayerDeath;
    }

    void OnPlayerDeath()
    {
        isDisabled = true;
    }

    
}