using UnityEngine;

public class SubirYBajar : MonoBehaviour
{
    [Header ("Player Top")]
    public GameObject puntoMover;     // Punto al que se moverá el jugador
    public GameObject Player;         // Referencia al jugador

    [Header("Player Lateral")]
    public GameObject PlayerL;
    public GameObject puntoMoverL;

    [Header("Settings")]
    public bool puedoPasarArriva = true;
    public bool puedoPasarAbajo = false;

    private bool haSalidoDelTrigger = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != Player) return;

        if (haSalidoDelTrigger)
        {
            if (puedoPasarArriva || puedoPasarAbajo)
            {
                Debug.Log(puedoPasarArriva ? "Subiendo..." : "Bajando...");
                Player.transform.position = puntoMover.transform.position;
                haSalidoDelTrigger = false;

                PlayerL.transform.position = puntoMoverL.transform.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
        {
            haSalidoDelTrigger = true;
        }
    }
}
