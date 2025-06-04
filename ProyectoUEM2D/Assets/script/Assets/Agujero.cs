using UnityEngine;


public class Agujero : MonoBehaviour
{
    public TopDownMovement playerTop;
    public GameObject player;
    private bool yaCayo = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Feets") && !yaCayo)
        {
            if (!playerTop.dashing)
            {
                yaCayo = true;

                // Mover al jugador
                player.transform.position = new Vector2(
                    player.transform.position.x,
                    player.transform.position.y - 21f
                );

                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Feets"))
        {
            yaCayo = false;
        }
    }

   
    
}
