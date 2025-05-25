using System.Collections;
using UnityEngine;
using Cinemachine;

public class Agujero : MonoBehaviour
{
    public TopDownMovement playerTop;
    public GameObject player;

    [Header("Cinemachine")]
    public CinemachineVirtualCamera virtualCam;        // Cámara actual
    public CinemachineVirtualCamera camaraNueva;       // Nueva cámara a activar
    public PolygonCollider2D nuevoConfiner;            // Nuevo confiner para la nueva cámara

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

                // Iniciar la transición
                StartCoroutine(CambiarConfinerYTransicion());
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

    private IEnumerator CambiarConfinerYTransicion()
    {
        yield return null;

        // Cambiar el confiner de la nueva cámara
        if (camaraNueva != null && nuevoConfiner != null)
        {
            CinemachineConfiner2D nuevoC = camaraNueva.GetComponent<CinemachineConfiner2D>();
            if (nuevoC != null)
            {
                nuevoC.enabled = false;
                yield return null;
                nuevoC.m_BoundingShape2D = nuevoConfiner;
                nuevoC.InvalidateCache();
                nuevoC.enabled = true;
            }
        }

        // TRANSICIÓN DE CÁMARA (cut)
        if (virtualCam != null)
            virtualCam.gameObject.SetActive(false);

        if (camaraNueva != null)
            camaraNueva.gameObject.SetActive(true);

        // TRANSICIÓN SUAVE DE FOLLOW (desactivar damping por 1s)
        CinemachineFramingTransposer transposer = camaraNueva.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (transposer != null)
        {
            float dampingOriginalX = transposer.m_XDamping;
            float dampingOriginalY = transposer.m_YDamping;

            transposer.m_XDamping = 0;
            transposer.m_YDamping = 0;

            yield return new WaitForSeconds(1f);

            transposer.m_XDamping = dampingOriginalX;
            transposer.m_YDamping = dampingOriginalY;
        }
    }
}
