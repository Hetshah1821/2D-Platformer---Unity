using UnityEngine;

public class AttackHitbox : MonoBehaviour 
{
    [SerializeField] private int damage = 1;
    

    private void OnTriggerEnter2D(Collider2D collision) 
     {
        if (collision.TryGetComponent(out EnemyHealth enemy)) 
        {
            enemy.TakeDamage(damage);
        }
    }

   
}