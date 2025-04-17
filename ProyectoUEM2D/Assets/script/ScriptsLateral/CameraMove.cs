using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Vector3 desplazamiento; // Ajustes para la posicion de la camara
    public float smoothTime = 0.25f;
    public Transform objetivo; // El objeto que seguir� la c�mara
    private Vector3 velocidad = Vector3.zero;

    private void LateUpdate() // Mejor usar LateUpdate para evitar jitter con el movimiento del jugador
    {
        if (objetivo != null)
        {
            Vector3 targetPos = objetivo.position + desplazamiento;
            targetPos.z = transform.position.z; // Mantener la Z de la c�mara fija

            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocidad, smoothTime);
        }
    }
}
