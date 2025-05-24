using System.Collections;
using UnityEngine;
using Cinemachine;

public class Agujero : MonoBehaviour
{
    public TopDownMovement playerTop;
    public GameObject player;

    [Header("Cinemachine")]
    public CinemachineVirtualCamera virtualCam;       // Cámara virtual
    public PolygonCollider2D nuevoConfiner;           // Confiner de la nueva sala

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

                // Hacemos el corte y cambiamos el confiner
                StartCoroutine(CambiarConfinerYTransicion());
            }
        }
    }

    private IEnumerator CambiarConfinerYTransicion()
    {
        // Esperamos un frame por si acaso
        yield return null;

        // Referencia al confiner
        CinemachineConfiner2D confiner = virtualCam.GetComponent<CinemachineConfiner2D>();
        if (confiner != null && nuevoConfiner != null)
        {
            confiner.enabled = false;
            yield return null;
            confiner.m_BoundingShape2D = nuevoConfiner;
            confiner.enabled = true;
        }

        // Referencia al Framing Transposer para modificar el damping
        CinemachineFramingTransposer transposer = virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (transposer != null)
        {
            // Guardamos el damping original
            float dampingOriginal = transposer.m_XDamping;

            // Apagamos el smoothing (corte duro)
            transposer.m_XDamping = 0;
            transposer.m_YDamping = 0;

            // Esperamos 1 segundo
            yield return new WaitForSeconds(1f);

            // Restauramos damping
            transposer.m_XDamping = dampingOriginal;
            transposer.m_YDamping = dampingOriginal; // opcional si Y también lo usas
        }
    }
}
