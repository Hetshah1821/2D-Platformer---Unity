using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float totalHealth = 100f;
     public float currentHealth { get; private set; }
    private Animator animate;
    private bool dead;
    [SerializeField]private float invulnerabilityDuration;
    [SerializeField] private int numberOFFades;
    private SpriteRenderer spritRend;
    [SerializeField]private Behaviour[] components;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake() 
    {
        currentHealth = totalHealth;
        animate = GetComponent<Animator>();
        spritRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage) 
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, totalHealth);

        if (currentHealth > 0) 
        {
            animate.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.Playsound(hurtSound);
        } 
        else if (!dead)
        {
            

            foreach (Behaviour component in components) 
            {
                component.enabled = false;
            }
            animate.SetBool("grounded", true);
            animate.SetTrigger("die");
            dead = true;
            SoundManager.instance.Playsound(deathSound);
        }
    }

    public void AddHealth(float value) 
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, totalHealth);
    }


    public void Respawn() 
    {
        dead = false;
        AddHealth(totalHealth);
        animate.ResetTrigger("die");
        animate.Play("Idle");
        StartCoroutine(Invunerability());

        foreach (Behaviour component in components) 
         {
            component.enabled = true;
        }
    }
    private IEnumerator Invunerability() 
    {
        Physics2D.IgnoreLayerCollision(7,8, true);
        for (int i = 0; i < numberOFFades; i++) 
        {
            spritRend.color = new Color(1,0,0, 0.5f);
            yield return new WaitForSeconds((invulnerabilityDuration / numberOFFades * 2));
            spritRend.color = Color.white;
            yield return new WaitForSeconds((invulnerabilityDuration / numberOFFades * 2));
        }
        Physics2D.IgnoreLayerCollision(7,8, false);
    }
   
}
