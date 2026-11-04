using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
   [SerializeField] private Transform previousStage;
   [SerializeField] private Transform nextStage;
   [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag ==  "Player") 
        {
            if (collision.transform.position.x < transform.position.x) 
            {
                cam.MoveToNextStage(nextStage);
                nextStage.GetComponent<Arena>().ActivateArena(true);
                previousStage.GetComponent<Arena>().ActivateArena(false);
            } 
           else 
            {
                cam.MoveToNextStage(previousStage);
                previousStage.GetComponent<Arena>().ActivateArena(true);
                nextStage.GetComponent<Arena>().ActivateArena(false);
            } 
        }
    }
}
