using UnityEditor.Experimental;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform enemy;
    [SerializeField] private float enemySpeed;
    private Vector3 initScale;
    private bool movingLeft;
    [SerializeField]private Animator animate;
    [SerializeField]private float idleDuration;
    private float idleTimer;

    private void Awake() 
    {
        initScale = enemy.localScale;
    }
    private void Update() 
    {
        if (enemy == null) 
        {
            return;
        }
        if (movingLeft) 
        {
            if (enemy.position.x >= leftEdge.position.x) 
            {
                MoveDirection(-1);
            } 
            else 
            {
                DirectionChange();
            }
        } else 
        {
            if (enemy.position.x <= rightEdge.position.x) 
            {
                MoveDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void OnDisable() 
    {
        if (animate != null) 
        {
            animate.SetBool("moving", false);
        }
    }

    private void DirectionChange() 
    {
        animate.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        
        if(idleTimer > idleDuration)
        movingLeft = !movingLeft;

    }
    private void MoveDirection(int direction) 
    {
        idleTimer = 0;
        animate.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * enemySpeed, enemy.position.y, enemy.position.z);
    }
}
