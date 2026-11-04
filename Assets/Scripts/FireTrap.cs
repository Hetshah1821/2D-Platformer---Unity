using UnityEngine;
using System.Collections;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float fireDamage;
    [SerializeField] private float actiationDelay;
    [SerializeField] private float activeTime;
    [SerializeField] private AudioClip fireSound;

    private Animator animate;
    private SpriteRenderer spritRend;
    private bool trigger;
    private bool isactive;

    private void Awake()
    {
        animate = GetComponent<Animator>();
        spritRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player") 
        {
            if (!trigger)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if (isactive) 
            {
                collision.GetComponent<HealthSystem>().TakeDamage(fireDamage);
            }
        }
    }

    private IEnumerator ActivateFireTrap() 
    { 
      trigger = true;
      spritRend.color = Color.red;
      yield return new WaitForSeconds(actiationDelay);
      spritRend.color = Color.white;
      isactive = true;
        animate.SetBool("activated", true);
        SoundManager.instance.Playsound(fireSound);
      yield return new WaitForSeconds(activeTime);
      isactive = false;
        trigger = false;
        animate.SetBool("activated", false);
    }
}
