using System.Collections;
using UnityEngine;
using Cinemachine;

public class Confiner : MonoBehaviour
{
    [Header("C�maras Cinemachine")]
    public CinemachineVirtualCamera camaraActual;
    public CinemachineVirtualCamera camaraNueva;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Entr� al trigger");

            // Apagar la c�mara actual
            if (camaraActual != null)
                camaraActual.gameObject.SetActive(false);

            // Encender la nueva c�mara
            if (camaraNueva != null)
                camaraNueva.gameObject.SetActive(true);
        }
    }
}