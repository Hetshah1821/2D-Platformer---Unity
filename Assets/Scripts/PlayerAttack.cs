using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 10;
    [SerializeField] private BoxCollider2D attackCollider;
    private Animator animate;
    private InputManager inputManager;
    private float cooldownTimer = 25f;
    [SerializeField] private AudioClip swordHit;

    private void Awake() 
    {
        animate = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        attackCollider.enabled = false;
    }

    private void Update() {

        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && inputManager.CanAttack() ) 
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack() 
    {
        SoundManager.instance.Playsound(swordHit);
        animate.SetTrigger("attack");
        cooldownTimer = 0f;
    }
    public void EnableAttack() 
    {
       
        attackCollider.enabled = true;
    }

    public void DisableAttack() 
    {
        
        attackCollider.enabled = false;
    }
}
