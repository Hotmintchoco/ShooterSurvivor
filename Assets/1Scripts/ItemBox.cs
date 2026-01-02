using UnityEngine;

public class ItemBox : LivingEntity
{
    public Item[] items;
    int index;

    protected override void Start()
    {
        base.Start();
        index = Random.Range(0, items.Length);
    }
    
    public override void Die()
    {
        Instantiate(items[index], transform.position, transform.rotation);
        base.Die();
    }
}
