using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
   [SerializeField] private float healthValue;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") 
        {
            SoundManager.instance.Playsound(pickupSound);
            collision.GetComponent<HealthSystem>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
