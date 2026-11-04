using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int enemyTotalHealth = 3;

    private int currentHealth;

    private void Awake() {
        currentHealth = enemyTotalHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
    }
}
