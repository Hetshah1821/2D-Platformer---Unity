using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private HealthSystem playerHealth;
    private UIManager uIManager;

    private void Awake() 
    {
        playerHealth = GetComponent<HealthSystem>();
        uIManager = FindAnyObjectByType<UIManager>();
    }

    public void CheckRespawn() 
    {
        if (currentCheckpoint == null) 
        {
            uIManager.GameOver();
            return;
        }
        
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();

        Camera.main.GetComponent<CameraController>().MoveToNextStage(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.transform.tag == "Checkpoint") 
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.Playsound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
