using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    public float health { get; protected set; }
    protected bool dead;
    protected Animator anim;

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        health = startingHealth;
    }

    public virtual void TakeHit(float damage, RaycastHit hit)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0 && !dead)
        {
            Die();
        }
        anim.SetTrigger("GetHit");

    }

    [ContextMenu("Self Destruct")]
    public virtual void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        Destroy(gameObject);
    }
}
