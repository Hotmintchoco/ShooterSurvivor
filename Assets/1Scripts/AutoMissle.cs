using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoMissle : MonoBehaviour
{
    public float attackTime = 1f;
    public Transform missileHold;
    public GameObject missile;

    private List<Transform> enemiesInRange = new List<Transform>();
    private float timer = 0f;

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
                timer = 0f; // 타이머 초기화
            }
        }
    }

    Transform GetNearestEnemy()
    {
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity; // 초기값은 무한대로 설정
        Vector3 myPos = transform.position;

        // 리스트를 거꾸로 돌면서(삭제 대응) 검사
        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            Transform enemy = enemiesInRange[i];

            // 1. 적이 죽어서 게임에서 사라졌다면(null) 리스트에서 제거
            if (enemy == null)
            {
                enemiesInRange.RemoveAt(i);
                continue;
            }

            // 2. 거리 계산
            float distanceToEnemy = Vector3.Distance(myPos, enemy.position);

            // 3. 현재까지 찾은 거리보다 더 가깝다면 갱신
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    // 적을 향해 총알 발사
    void Shoot(Transform target)
    {
        if (missile == null) return;

        // 총알 생성
        GameObject newMissile = Instantiate(missile, missileHold.position, Quaternion.identity);
        
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
