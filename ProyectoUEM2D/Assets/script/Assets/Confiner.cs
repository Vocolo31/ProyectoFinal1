using UnityEngine;
using Cinemachine;

public class Confiner : MonoBehaviour
{
    [Header("Cámaras Cinemachine")]
    public CinemachineVirtualCamera camaraActual;
    public CinemachineVirtualCamera camaraNueva;

    [Header("Nuevo Confiner")]
    public Collider2D nuevoConfiner;

    private void Start()
    {
        // Asegurarse de que Cinemachine arranca con la cámara actual activa
        if (camaraActual != null)
            camaraActual.gameObject.SetActive(true);

        if (camaraNueva != null)
            camaraNueva.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTop"))
        {
            Debug.Log("Entró al trigger");

            // Cambiar el confiner de la nueva cámara
            CinemachineConfiner2D confiner = camaraNueva.GetComponent<CinemachineConfiner2D>();
            if (confiner != null && nuevoConfiner != null)
            {
                confiner.m_BoundingShape2D = nuevoConfiner;
                confiner.InvalidateCache(); // Necesario para aplicar el nuevo collider
                Debug.Log("✅ Confiner cambiado correctamente.");
            }
            else
            {
                Debug.LogWarning("⚠️ No se pudo cambiar el confiner: falta asignar uno o ambos.");
            }

            // Activar la nueva cámara y desactivar la actual
            if (camaraActual != null)
                camaraActual.gameObject.SetActive(false);

            if (camaraNueva != null)
                camaraNueva.gameObject.SetActive(true);
        }
    }
}
