using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private int attackDamage = 100;
    [SerializeField] private Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool goHit = damageable.Hit(attackDamage, deliveredKnockback);

            if (goHit)
                Debug.Log(collision.name + "hit for" + attackDamage);
        }
    }
}
