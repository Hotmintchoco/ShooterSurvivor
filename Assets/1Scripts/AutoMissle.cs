using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoMissle : MonoBehaviour
{
    public float attackTime = 1f;
    public Transform missileHold;
    public Projectile missile;
    public float damage = 1f;

    List<Transform> enemiesInRange = new List<Transform>();
    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // 공격 시간이 되었을 때
        if (timer >= attackTime)
        {
            Transform target = GetNearestEnemy();
            
            if (target != null)
            {
                Shoot(target);
                timer = 0;
            }
        }
    }

    Transform GetNearestEnemy()
    {
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity; // 초기값은 무한대로 설정
        Vector3 myPos = transform.position;

        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            Transform enemy = enemiesInRange[i];

            if (enemy == null)
            {
                enemiesInRange.RemoveAt(i);
                continue;
            }

            float distanceToEnemy = Vector3.Distance(myPos, enemy.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    void Shoot(Transform target)
    {
        if (missile == null) return;

        Projectile newMissile = Instantiate(missile, missileHold.position, Quaternion.identity);
        newMissile.SetDamage(damage);
        
        Vector3 targetPosition = target.position;
        targetPosition.y = missileHold.position.y;

        newMissile.transform.LookAt(targetPosition);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(other.transform))
            {
                enemiesInRange.Add(other.transform);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (enemiesInRange.Contains(other.transform))
            {
                enemiesInRange.Remove(other.transform);
            }
        }
    }
}
