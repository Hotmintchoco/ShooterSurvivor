using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    NavMeshAgent pathfinder;
    Transform target;
    Rigidbody rigid;
    
    bool isChase;
    float refreshRate = 0.25f;

    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody>();

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
        StartCoroutine(KnockBack());
        base.TakeHit(damage, hit);
    }

    IEnumerator UpdatePath()
    {
        while (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            
            if (isChase && !dead)
                pathfinder.SetDestination(targetPosition);
            
            yield return new WaitForSeconds(refreshRate);
        }
    }

    IEnumerator KnockBack()
    {
        yield return null;
        Vector3 playerPos = new Vector3(target.position.x, 0, target.position.z);
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode.Impulse);
    }
}
