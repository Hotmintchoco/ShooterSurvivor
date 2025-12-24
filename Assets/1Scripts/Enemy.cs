using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    NavMeshAgent pathfinder;
    Transform target;
    
    bool isChase;
    bool isHit;
    float refreshRate = 0.25f;

    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdatePath());

        Invoke("ChaseStart", 1);
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    void Update()
    {
        
    }

    public override void TakeHit(float damage, RaycastHit hit)
    {
        StartCoroutine(HitStop());
        base.TakeHit(damage, hit);
    }
    
    IEnumerator UpdatePath()
    {
        while (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            
            if (isChase && !dead && !isHit)
                pathfinder.SetDestination(targetPosition);
            
            yield return new WaitForSeconds(refreshRate);
        }
    }

    IEnumerator HitStop()
    {
        isHit = true;
        pathfinder.isStopped = true; // 이동 멈춤
        pathfinder.velocity = Vector3.zero; // 현재 관성으로 미끄러지는 것 방지

        // 0.5초 정도 멈춰있게 설정 (원하는 시간만큼 조절)
        yield return new WaitForSeconds(0.5f);

        if (!dead) // 죽지 않았다면 다시 이동 재개
        {
            isHit = false;
            pathfinder.isStopped = false; // 이동 다시 시작
        }
    }
}
