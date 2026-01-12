using UnityEngine;

public class ItemBox : LivingEntity
{
    public Item[] items;
    int index;

    protected override void Start()
    {
        base.Start();

        // 레벨이 높아짐에 따라 박스의 보상이 더 좋게 나올 수 있음.
        var instance = GameManager.instance;
        int maxIndex = Mathf.Min(instance.level + 1, items.Length);

        index = Random.Range(0, maxIndex);
        Item item = items[index];

        print(index);

        // exp를 얻을 때 처리
        if (item.type == Item.Type.Exp)
        {
            print("expLevel = " + instance.level);
            item.value = instance.nextExp[instance.level];
        }
    }
    
    public override void Die()
    {
        Instantiate(items[index], transform.position, transform.rotation);
        base.Die();
    }
}
