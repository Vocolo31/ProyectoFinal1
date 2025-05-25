using System.Collections;
using UnityEngine;
using Cinemachine;

public class Confiner : MonoBehaviour
{
    [Header("Cámaras Cinemachine")]
    public CinemachineVirtualCamera camaraActual;
    public CinemachineVirtualCamera camaraNueva;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Entró al trigger");

            // Apagar la cámara actual
            if (camaraActual != null)
                camaraActual.gameObject.SetActive(false);

            // Encender la nueva cámara
            if (camaraNueva != null)
                camaraNueva.gameObject.SetActive(true);
        }
    }
}