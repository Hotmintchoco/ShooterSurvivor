using UnityEngine;

public class DivineAura : MonoBehaviour
{
    public float damage = 3f;

    void OnTriggerStay(Collider other)
    {
        // 플레이어 자신에게는 데미지를 주지 않도록 방어
        if (other.gameObject == transform.parent.gameObject) return;

        IDamageable target = other.GetComponent<IDamageable>();
        
        if (target != null && other.CompareTag("Enemy"))
        {
            target.TakeDamage(damage * Time.deltaTime);
        }
    }
}
