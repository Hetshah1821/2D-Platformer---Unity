using UnityEngine;

public class SawTrap : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float damage;
    private bool movesLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake() 
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void FixedUpdate() 
    {
        if (movesLeft) {
            if (transform.position.x > leftEdge) 
            {
                transform.position = new Vector3(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            } else 
            {
                movesLeft = false;
            }
        } else 
        {
            if (transform.position.x < rightEdge) 
            {
                transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            } else 
            {
                movesLeft = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        
       
        if (collision.tag == "Player") 
        {
            collision.GetComponent<HealthSystem>().TakeDamage(damage);
        }
    }

   
}
