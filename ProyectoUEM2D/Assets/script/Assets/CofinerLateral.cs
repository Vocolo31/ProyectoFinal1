using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofinerLateral : MonoBehaviour
{
    [Header("C�maras Cinemachine")]
    public CinemachineVirtualCamera camaraActual;
    public CinemachineVirtualCamera camaraNueva;

    public bool InNewCamera = false;

    [Header("Nuevo Confiner")]
    public Collider2D nuevoConfiner;

    private void Start()
    {
        // Asegurarse de que Cinemachine arranca con la c�mara actual activa
        if (camaraActual != null)
            camaraActual.gameObject.SetActive(true);

        if (camaraNueva != null)
            camaraNueva.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("SIIIIIIIIIIIIIIIIIIIIIII");
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.J))
        {
            Debug.Log("Entr� al lateral");

            // Cambiar el confiner de la nueva c�mara
            CinemachineConfiner2D confiner = camaraNueva.GetComponent<CinemachineConfiner2D>();
            if (confiner != null && nuevoConfiner != null)
            {
                confiner.m_BoundingShape2D = nuevoConfiner;
                confiner.InvalidateCache(); // Necesario para aplicar el nuevo collider
                Debug.Log("Confiner cambiado correctamente.");
            }
            else
            {
                Debug.LogWarning(" No se pudo cambiar el confiner: falta asignar uno o ambos.");
            }

            // Activar la nueva c�mara y desactivar la actual
            if (camaraActual != null)
            {
                camaraActual.gameObject.SetActive(false);

                InNewCamera = true;

            }


            if (camaraNueva != null)
            {
                camaraNueva.gameObject.SetActive(true);

                InNewCamera = false;
            }


        }
    }
}

