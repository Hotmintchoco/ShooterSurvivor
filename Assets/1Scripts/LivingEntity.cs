using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    protected float health;
    protected bool dead;
    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        health = startingHealth;
    }

    public virtual void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;
        anim.SetTrigger("GetHit");

        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    protected void Die()
    {
        dead = true;
        Destroy(gameObject);
    }
}
