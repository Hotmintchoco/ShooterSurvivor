using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State { Idle, Chasing }
    State currentState;

    public int exp = 1;
    public float damage = 1f;
    public Item expItem;
    NavMeshAgent pathfinder;
    Transform target;
    LivingEntity targetEntity;
    
    bool isHit;
    bool hasTarget;
    float refreshRate = 0.25f;
    float nextAttackTime;

    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetEntity = target.GetComponent<LivingEntity>();
        targetEntity.OnDeath += OnTargetDeath;
        expItem.value = exp;
        hasTarget = true;

        StartCoroutine(UpdatePath());

        Invoke("ChaseStart", 0.25f);
    }

    void Update()
    {
        
    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
        if (anim != null)
            anim.SetBool("isWalk", false);
    }

    void ChaseStart()
    {
        currentState = State.Chasing;
        anim.SetBool("isWalk", true);
    }


    void OnTriggerStay(Collider other)
    {
        float tic = 0.5f;
        if (other.gameObject == target.gameObject && Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + tic;
            targetEntity.TakeDamage(damage);
        }
    }

    public override void TakeHit(float damage, RaycastHit hit)
    {
        AudioManager.instance.PlaySound("Impact", transform.position);
        if (dead) return;
        base.TakeHit(damage, hit);
        if (health > 0)
            StartCoroutine(HitStop());
        else
        {
            AudioManager.instance.PlaySound("Enemy Death", transform.position);
        }

        if (anim)
            anim.SetTrigger("GetHit");
    }

    public override void Die()
    {
        Instantiate(expItem, transform.position, transform.rotation);
        base.Die();
    }
    
    IEnumerator UpdatePath()
    {
        while (hasTarget)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            
            if (currentState == State.Chasing && !dead && !isHit)
                pathfinder.SetDestination(targetPosition);
            
            yield return new WaitForSeconds(refreshRate);
        }
    }

    IEnumerator HitStop()
    {
        isHit = true;
        pathfinder.isStopped = true;
        pathfinder.velocity = Vector3.zero; // 현재 관성으로 미끄러지는 것 방지

        yield return new WaitForSeconds(0.5f);

        if (!dead && isHit)
        {
            isHit = false;
            pathfinder.isStopped = false;
        }
    }
}
