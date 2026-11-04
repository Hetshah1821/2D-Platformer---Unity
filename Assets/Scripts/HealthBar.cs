using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHelathBar;

    private void Start() 
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update() 
    {
        currentHelathBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
